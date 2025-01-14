import React, { useState, useEffect } from "react";
import {fetchWithAuth} from "../services/authService";

function GenreSelect({ selectedGenres, setSelectedGenres }) {
    const [genres, setGenres] = useState([]);

    useEffect(() => {
        const fetchGenres = async () => {
            try {
                const response = await fetchWithAuth("http://localhost:5249/api/movies/get-all-genres");
                const data = await response.json();
                setGenres(data);
            } catch (error) {
                console.error("Error fetching genres:", error);
            }
        };

        fetchGenres();
    }, []);

    const handleChange = (e) => {
        const options = e.target.options;
        const selected = Array.from(options)
            .filter((option) => option.selected)
            .map((option) => option.value);

        setSelectedGenres(selected);
    };

    return (
        <select id="genres" name="genres" size="4" multiple onChange={handleChange}>
            {genres.map((genre, index) => (
                <option key={index} value={genre.name}>
                    {genre.name}
                </option>
            ))}
        </select>
    );
}

export default GenreSelect;
