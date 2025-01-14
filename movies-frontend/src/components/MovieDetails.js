import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import MoviePoster from "./MoviePoster";
import MovieInfo from "./MovieInfo";
import MovieCast from "./MovieCast";
import MovieReviews from "./MovieReviews";
import Footer from "./Footer";
import Header from "./Header";
import {fetchWithAuth} from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function MovieDetails() {
    const { tmdbId } = useParams();
    const [movie, setMovie] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const { t, i18n } = useTranslation();

    useEffect(() => {
        const fetchMovie = async () => {
            try {
                const response = await fetchWithAuth(`http://localhost:5249/api/movies/get-movie-by-tmdb-id?tmdbId=${tmdbId}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                const data = await response.json();
                setMovie(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchMovie();
    }, [tmdbId]);

    if (loading) return <p>{t("Loading")}...</p>;
    if (error) return <p>Error: {error}</p>;
    if (!movie) return <p>Movie not found</p>;

    return (
        <>
            <Header />
            <main id="movie-info-main-section">
                <h1 id="movie-title">{movie.title}</h1>
                <div id="movie-detailed-info">
                    <MoviePoster posterUrl={movie.posterUrl} title={movie.title}/>
                    <MovieInfo movie={movie}/>
                </div>
                <h2>{t("Top Billed Cast")}</h2>
                <MovieCast actors={movie.actors}/>
                <MovieReviews movieId={movie.id}/>
            </main>
            <Footer />
        </>
    );
}

export default MovieDetails;
