import React, { useEffect, useState } from 'react';
import MovieIntro from "./MovieIntro";
import Header from "./Header";
import Footer from "./Footer";
import { fetchWithAuth } from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function Home() {
    const { t, i18n } = useTranslation();
    const [movies, setMovies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);
    const [moviesPerPage] = useState(4);
    const [totalMovies, setTotalMovies] = useState(0);

    const fetchMovies = async () => {
        setLoading(true);
        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/movies/get-all-movies-segmented-with-count?page=${currentPage}&pageSize=${moviesPerPage}`);
            const data = await response.json();
            setMovies(data.movies);
            setTotalMovies(data.totalCount);
        } catch (error) {
            console.error('Error fetching movies:', error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchMovies();
    }, [currentPage]);

    const paginate = (pageNumber) => setCurrentPage(pageNumber);

    return (
        <>
            <Header />
            <main id="main-section">
                <h1>{t("All available movies")}</h1>

                {loading ? (
                    <p>{t("Loading")}...</p>
                ) : (
                    <>
                        <div id="movies-list">
                            {movies.map((movie) => (
                                <MovieIntro key={movie.id} movie={movie} />
                            ))}
                        </div>

                        <div className="pagination">
                            {[...Array(Math.ceil(totalMovies / moviesPerPage))].map((_, index) => (
                                <button
                                    key={index + 1}
                                    onClick={() => paginate(index + 1)}
                                    className={currentPage === index + 1 ? 'active' : ''}
                                >
                                    {index + 1}
                                </button>
                            ))}
                        </div>
                    </>
                )}
            </main>
            <Footer />
        </>
    );
}

export default Home;
