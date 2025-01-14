import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import Header from "./Header";
import MovieIntro from "./MovieIntro";
import Footer from "./Footer";
import {fetchWithAuth} from "../services/authService";

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const SearchPage = () => {
    const query = useQuery().get('query');
    const [results, setResults] = useState([]);

    useEffect(() => {
        const fetchResults = async () => {
            try {
                const response = await fetchWithAuth(`http://localhost:5249/api/movies/search?query=${encodeURIComponent(query)}`);
                if (response.ok) {
                    const data = await response.json();
                    setResults(data);
                } else {
                    console.error('Search failed');
                }
            } catch (error) {
                console.error('Error fetching search results:', error);
            }
        };

        if (query) {
            fetchResults();
        }
    }, [query]);

    return (
        <>
            <Header/>
            <main id="main-section">
                <h1>Found movies</h1>

                <div id="movies-list">
                    {results.map((movie) => (
                        <MovieIntro key={movie.title} movie={movie}/>
                    ))}
                </div>
            </main>
            <Footer />
        </>
    );
};

export default SearchPage;
