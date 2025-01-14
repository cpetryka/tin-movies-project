import React, { useState, useEffect } from "react";
import Header from "./Header";
import Footer from "./Footer";
import GenreSelect from "./GenreSelect";
import ActorWithRoleSelect from "./ActorSelect";
import { fetchWithAuth } from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function ManageMovies() {
    const { t, i18n } = useTranslation();
    const [movieData, setMovieData] = useState({
        title: "",
        director: "",
        releaseDate: "",
        duration: 0,
        tmdbId: "",
        posterUrl: "",
        genres: [],
        actors: [],
    });
    const [movies, setMovies] = useState([]);
    const [selectedMovieUpdate, setSelectedMovieUpdate] = useState(null);
    const [selectedMovieDelete, setSelectedMovieDelete] = useState(null);
    const [editedMovieData, setEditedMovieData] = useState({
        title: "",
        director: "",
        releaseDate: "",
        duration: 0,
        tmdbId: "",
        posterUrl: "",
        genres: [],
        actors: [],
    });
    const [loading, setLoading] = useState(false);
    const [successMessageAdd, setSuccessMessageAdd] = useState("");
    const [successMessageEdit, setSuccessMessageEdit] = useState("");
    const [successMessageDelete, setSuccessMessageDelete] = useState("");
    const [errorMessageAdd, setErrorMessageAdd] = useState("");
    const [errorMessageEdit, setErrorMessageEdit] = useState("");
    const [errorMessageDelete, setErrorMessageDelete] = useState("");

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const response = await fetchWithAuth("http://localhost:5249/api/movies/get-all-movies");
                if (response.ok) {
                    const data = await response.json();
                    setMovies(data);
                } else {
                    throw new Error("Failed to fetch movies.");
                }
            } catch (error) {
                setErrorMessageEdit(t("An error occurred while fetching movies."));
                setErrorMessageDelete(t("An error occurred while fetching movies."));
            }
        };

        fetchMovies();
    }, []);

    const handleInputChange = (e, setData, data) => {
        const { name, value } = e.target;
        setData({ ...data, [name]: value });
    };

    const handleAddMovie = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageAdd("");
        setErrorMessageAdd("");

        const payload = {
            ...movieData,
            duration: parseInt(movieData.duration, 10),
        };

        try {
            const response = await fetchWithAuth("http://localhost:5249/api/movies/add-new-movie", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                setSuccessMessageAdd(t("Movie added successfully!"));
                setMovieData({
                    title: "",
                    director: "",
                    releaseDate: "",
                    duration: 0,
                    tmdbId: "",
                    posterUrl: "",
                    genres: [],
                    actors: [],
                });
                const updatedMovies = await fetchWithAuth("http://localhost:5249/api/movies/get-all-movies");
                setMovies(await updatedMovies.json());
            } else {
                setErrorMessageAdd(t("Failed to add movie. Please check the input data."));
            }
        } catch (error) {
            setErrorMessageAdd(t("An error occurred while adding the movie."));
        } finally {
            setLoading(false);
        }
    };

    const handleEditMovie = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageEdit("");
        setErrorMessageEdit("");

        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/movies/update-movie-by-id?movieId=${selectedMovieUpdate.id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(editedMovieData),
            });

            if (response.ok) {
                setSuccessMessageEdit(t("Movie updated successfully!"));
                setEditedMovieData({
                    title: "",
                    director: "",
                    releaseDate: "",
                    duration: 0,
                    tmdbId: "",
                    posterUrl: "",
                    genres: [],
                    actors: [],
                });
                setSelectedMovieUpdate(null);
                const updatedMovies = await fetchWithAuth("http://localhost:5249/api/movies/get-all-movies");
                setMovies(await updatedMovies.json());
            } else {
                setErrorMessageEdit(t("Failed to update movie."));
            }
        } catch (error) {
            setErrorMessageEdit(t("An error occurred while updating the movie."));
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteMovie = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageDelete("");
        setErrorMessageDelete("");

        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/movies/delete-movie-by-id?movieId=${selectedMovieDelete.id}`, {
                method: "DELETE",
            });

            if (response.ok) {
                setSuccessMessageDelete(t("Movie deleted successfully!"));
                setSelectedMovieDelete(null);
                const updatedMovies = await fetchWithAuth("http://localhost:5249/api/movies/get-all-movies");
                setMovies(await updatedMovies.json());
            } else {
                setErrorMessageDelete(t("Failed to delete movie."));
            }
        } catch (error) {
            setErrorMessageDelete(t("An error occurred while deleting the movie."));
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Header />
            <main id="main-section">
                <h1>{t("Add New Movie")}</h1>
                <form onSubmit={handleAddMovie} id="add-new-entity-form">
                    <label htmlFor="title">{t("Title")}:</label>
                    <input
                        type="text"
                        name="title"
                        id="title"
                        value={movieData.title}
                        onChange={(e) => handleInputChange(e, setMovieData, movieData)}
                        required
                    />
                    <br/>
                    <label htmlFor="director">{t("Director")}:</label>
                    <input
                        type="text"
                        name="director"
                        id="director"
                        value={movieData.director}
                        onChange={(e) => handleInputChange(e, setMovieData, movieData)}
                        required
                    />
                    <br/>
                    <label htmlFor="releaseDate">{t("Release Date")}:</label>
                    <input
                        type="date"
                        name="releaseDate"
                        id="releaseDate"
                        value={movieData.releaseDate}
                        onChange={(e) => handleInputChange(e, setMovieData, movieData)}
                        required
                    />
                    <br/>
                    <label htmlFor="duration">{t("Duration (in minutes)")}:</label>
                    <input
                        type="number"
                        name="duration"
                        id="duration"
                        value={movieData.duration}
                        onChange={(e) => handleInputChange(e, setMovieData, movieData)}
                        required
                    />
                    <br/>
                    <label htmlFor="genres">{t("Genres")}:</label>
                    <GenreSelect
                        selectedGenres={movieData.genres}
                        setSelectedGenres={(genres) =>
                            setMovieData({...movieData, genres})
                        }
                    />
                    <br/>
                    <label htmlFor="actors">{t("Actors")}:</label>
                    <ActorWithRoleSelect
                        selectedActors={movieData.actors}
                        setSelectedActors={(actors) =>
                            setMovieData({...movieData, actors})
                        }
                    />
                    <br/>
                    <label htmlFor="tmdbId">Tmdb Id:</label>
                    <input
                        type="text"
                        name="tmdbId"
                        id="tmdbId"
                        value={movieData.tmdbId}
                        onChange={(e) => handleInputChange(e, setMovieData, movieData)}
                        required
                    />
                    <br/>
                    <label htmlFor="posterUrl">{t("Poster URL")}:</label>
                    <input
                        type="text"
                        name="posterUrl"
                        id="posterUrl"
                        value={movieData.posterUrl}
                        onChange={(e) => handleInputChange(e, setMovieData, movieData)}
                        required
                    />
                    <br/>
                    <button type="submit" disabled={loading}>{t("Add Movie")}</button>
                    {successMessageAdd && <p className="success-message">{successMessageAdd}</p>}
                    {errorMessageAdd && <p className="error-message">{errorMessageAdd}</p>}
                </form>

                <div className="separator"></div>

                <h1>{t("Update Selected Movie")}</h1>
                <form onSubmit={handleEditMovie} id="add-new-entity-form">
                    <label htmlFor="selectedMovie">{t("Select Movie")}:</label>
                    <select
                        name="selectedMovie"
                        id="selectedMovie"
                        value={selectedMovieUpdate?.id || ""}
                        onChange={(e) => {
                            const actor = movies.find((a) => a.id === parseInt(e.target.value, 10));
                            setSelectedMovieUpdate(actor);
                            setEditedMovieData(actor || {
                                name: "",
                                genderId: 0,
                                birthDate: "",
                                deathDate: "",
                                biography: ""
                            });
                        }}
                    >
                        <option value="" disabled>
                            {t("-- Select Movie --")}
                        </option>
                        {movies.map((actor) => (
                            <option key={actor.id} value={actor.id}>
                                {actor.title}
                            </option>
                        ))}
                    </select>

                    {selectedMovieUpdate && (
                        <>
                            <br/>
                            <label htmlFor="title">{t("Title")}:</label>
                            <input
                                type="text"
                                name="title"
                                id="title"
                                value={editedMovieData.title}
                                onChange={(e) => handleInputChange(e, setEditedMovieData, editedMovieData)}
                                required
                            />
                            <br/>
                            <label htmlFor="director">{t("Director")}:</label>
                            <input
                                type="text"
                                name="director"
                                id="director"
                                value={editedMovieData.director}
                                onChange={(e) => handleInputChange(e, setEditedMovieData, editedMovieData)}
                                required
                            />
                            <br/>
                            <label htmlFor="releaseDate">{t("Release Date")}:</label>
                            <input
                                type="date"
                                name="releaseDate"
                                id="releaseDate"
                                value={editedMovieData.releaseDate}
                                onChange={(e) => handleInputChange(e, setEditedMovieData, editedMovieData)}
                                required
                            />
                            <br/>
                            <label htmlFor="duration">{t("Duration (in minutes)")}:</label>
                            <input
                                type="number"
                                name="duration"
                                id="duration"
                                value={editedMovieData.duration}
                                onChange={(e) => handleInputChange(e, setEditedMovieData, editedMovieData)}
                                required
                            />
                            <br/>
                            <label htmlFor="genres">{t("Genres")}:</label>
                            <GenreSelect
                                selectedGenres={editedMovieData.genres}
                                setSelectedGenres={(genres) =>
                                    setEditedMovieData({...editedMovieData, genres})
                                }
                            />
                            <br/>
                            <label htmlFor="actors">{t("Actors")}:</label>
                            <ActorWithRoleSelect
                                selectedActors={editedMovieData.actors}
                                setSelectedActors={(actors) =>
                                    setEditedMovieData({...editedMovieData, actors})
                                }
                            />
                            <br/>
                            <label htmlFor="tmdbId">Tmdb Id:</label>
                            <input
                                type="text"
                                name="tmdbId"
                                id="tmdbId"
                                value={editedMovieData.tmdbId}
                                onChange={(e) => handleInputChange(e, setEditedMovieData, editedMovieData)}
                                required
                            />
                            <br/>
                            <label htmlFor="posterUrl">{t("Poster URL")}:</label>
                            <input
                                type="text"
                                name="posterUrl"
                                id="posterUrl"
                                value={editedMovieData.posterUrl}
                                onChange={(e) => handleInputChange(e, setEditedMovieData, editedMovieData)}
                                required
                            />
                            <br/>
                            <button type="submit" disabled={loading}>
                                {loading ? t("Updating...") : t("Update Movie")}
                            </button>
                        </>
                    )}

                    {successMessageEdit && <p className="success-message">{successMessageEdit}</p>}
                    {errorMessageEdit && <p className="error-message">{errorMessageEdit}</p>}
                </form>

                <div className="separator"></div>

                <h1>{t("Delete Selected Movie")}</h1>
                <form onSubmit={handleDeleteMovie} id="add-new-entity-form">
                    <label htmlFor="selectedMovieDelete">{t("Select Movie")}:</label>
                    <select
                        name="selectedMovieDelete"
                        id="selectedMovieDelete"
                        value={selectedMovieDelete?.id || ""}
                        onChange={(e) => {
                            const actor = movies.find((a) => a.id === parseInt(e.target.value, 10));
                            setSelectedMovieDelete(actor);
                        }}
                    >
                        <option value="" disabled>
                            {t("-- Select Movie --")}
                        </option>
                        {movies.map((movie) => (
                            <option key={movie.id} value={movie.id}>
                                {movie.title}
                            </option>
                        ))}
                    </select>
                    <br/>

                    {selectedMovieDelete && (
                        <button type="submit" disabled={loading}>
                            {loading ? t("Deleting...") : t("Delete Movie")}
                        </button>
                    )}

                    {successMessageDelete && <p className="success-message">{successMessageDelete}</p>}
                    {errorMessageDelete && <p className="error-message">{errorMessageDelete}</p>}
                </form>
            </main>
            <Footer/>
        </>
    );
}

export default ManageMovies;
