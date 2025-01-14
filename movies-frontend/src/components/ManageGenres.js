import React, { useState, useEffect } from "react";
import Header from "./Header";
import Footer from "./Footer";
import { fetchWithAuth } from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function ManageGenres() {
    const { t, i18n } = useTranslation();
    const [newGenreName, setNewGenreName] = useState("");
    const [genres, setGenres] = useState([]);
    const [selectedGenreUpdate, setSelectedGenreUpdate] = useState(null);
    const [selectedGenreDelete, setSelectedGenreDelete] = useState(null);
    const [editedGenreName, setEditedGenreName] = useState("");
    const [loading, setLoading] = useState(false);
    const [successMessageAdd, setSuccessMessageAdd] = useState("");
    const [errorMessageAdd, setErrorMessageAdd] = useState("");
    const [successMessageUpdate, setSuccessMessageUpdate] = useState("");
    const [errorMessageUpdate, setErrorMessageUpdate] = useState("");
    const [successMessageDelete, setSuccessMessageDelete] = useState("");
    const [errorMessageDelete, setErrorMessageDelete] = useState("");

    useEffect(() => {
        const fetchGenres = async () => {
            try {
                const response = await fetchWithAuth("http://localhost:5249/api/movies/get-all-genres");
                if (response.ok) {
                    const data = await response.json();
                    setGenres(data);
                } else {
                    throw new Error(t("Failed to fetch genres."));
                }
            } catch (error) {
                setErrorMessageUpdate(t("An error occurred while fetching genres."));
            }
        };

        fetchGenres();
    }, []);

    const handleAddGenre = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageAdd("");
        setErrorMessageAdd("");

        try {
            const response = await fetchWithAuth("http://localhost:5249/api/movies/add-new-genre", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(newGenreName),
            });

            if (response.ok) {
                setSuccessMessageAdd(t("Genre added successfully!"));
                setNewGenreName("");
                const updatedGenres = await fetchWithAuth("http://localhost:5249/api/movies/get-all-genres");
                setGenres(await updatedGenres.json());
            } else {
                const errorData = await response.json();
                setErrorMessageAdd(errorData.title || t("Failed to add genre."));
            }
        } catch (error) {
            setErrorMessageAdd(t("An error occurred while adding the genre."));
        } finally {
            setLoading(false);
        }
    };

    const handleEditGenre = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageUpdate("");
        setErrorMessageUpdate("");

        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/movies/update-genre-by-id?genreId=${selectedGenreUpdate.id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(editedGenreName),
            });

            if (response.ok) {
                setSuccessMessageUpdate(t("Genre updated successfully!"));
                setEditedGenreName("");
                setSelectedGenreUpdate(null);
                setSelectedGenreDelete(null);
                const updatedGenres = await fetchWithAuth("http://localhost:5249/api/movies/get-all-genres");
                setGenres(await updatedGenres.json());
            } else {
                setErrorMessageUpdate(t("Failed to update genre."));
            }
        } catch (error) {
            setErrorMessageUpdate(t("An error occurred while updating the genre."));
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteGenre = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageDelete("");
        setErrorMessageDelete("");

        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/movies/delete-genre-by-id?genreId=${selectedGenreDelete.id}`, {
                method: "DELETE",
            });

            if (response.ok) {
                setSuccessMessageDelete(t("Genre deleted successfully!"));
                setSelectedGenreUpdate(null);
                setSelectedGenreDelete(null);
                const updatedGenres = await fetchWithAuth("http://localhost:5249/api/movies/get-all-genres");
                setGenres(await updatedGenres.json());
            } else {
                setErrorMessageDelete(t("Failed to delete genre."));
            }
        } catch (error) {
            setErrorMessageDelete(t("An error occurred while deleting the genre."));
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Header />
            <main id="main-section">
                <h1>{t("Add New Genre")}</h1>
                {/* Add New Genre Form */}
                <form onSubmit={handleAddGenre} id="add-new-entity-form">
                    <label htmlFor="newGenreName">{t("Genre Name")}:</label>
                    <input
                        type="text"
                        name="newGenreName"
                        id="newGenreName"
                        value={newGenreName}
                        onChange={(e) => setNewGenreName(e.target.value)}
                        required
                    />
                    <button type="submit" disabled={loading}>
                        {loading ? t("Adding...") : t("Add Genre")}
                    </button>
                    {successMessageAdd && <p className="success-message">{successMessageAdd}</p>}
                    {errorMessageAdd && <p className="error-message">{errorMessageAdd}</p>}
                </form>

                <div className={"separator"}></div>

                <h1>{t("Update Selected Genre")}</h1>
                {/* Edit Existing Genre Form */}
                <form onSubmit={handleEditGenre} id="add-new-entity-form">
                    <label htmlFor="selectedGenre">{t("Select Genre")}:</label>
                    <select
                        name="selectedGenre"
                        id="selectedGenre"
                        value={selectedGenreUpdate?.id || ""}
                        onChange={(e) => {
                            const genre = genres.find((g) => g.id === parseInt(e.target.value, 10));
                            setSelectedGenreUpdate(genre);
                            setEditedGenreName(genre ? genre.name : "");
                        }}
                    >
                        <option value="" disabled>
                            {t("-- Select Genre --")}
                        </option>
                        {genres.map((genre) => (
                            <option key={genre.id} value={genre.id}>
                                {genre.name}
                            </option>
                        ))}
                    </select>

                    {selectedGenreUpdate && (
                        <>
                            <label htmlFor="editedGenreName">{t("Edit Name")}:</label>
                            <input
                                type="text"
                                name="editedGenreName"
                                id="editedGenreName"
                                value={editedGenreName}
                                onChange={(e) => setEditedGenreName(e.target.value)}
                                required
                            />
                            <button type="submit" disabled={loading}>
                                {loading ? t("Updating...") : t("Update Genre")}
                            </button>
                        </>
                    )}
                    {successMessageUpdate && <p className="success-message">{successMessageUpdate}</p>}
                    {errorMessageUpdate && <p className="error-message">{errorMessageUpdate}</p>}
                </form>

                <div className={"separator"}></div>

                <h1>{t("Delete Selected Genre")}</h1>
                {/* Delete Genre Form */}
                <form onSubmit={handleDeleteGenre} id="add-new-entity-form">
                    <label htmlFor="selectedGenreToDelete">{t("Select Genre")}:</label>
                    <select
                        name="selectedGenreToDelete"
                        id="selectedGenreToDelete"
                        value={selectedGenreDelete?.id || ""}
                        onChange={(e) => {
                            const genre = genres.find((g) => g.id === parseInt(e.target.value, 10));
                            setSelectedGenreDelete(genre);
                        }}
                    >
                        <option value="" disabled>
                            {t("-- Select Genre --")}
                        </option>
                        {genres.map((genre) => (
                            <option key={genre.id} value={genre.id}>
                                {genre.name}
                            </option>
                        ))}
                    </select>

                    {selectedGenreDelete && (
                        <button type="submit" disabled={loading}>
                            {loading ? t("Deleting...") : t("Delete Genre")}
                        </button>
                    )}
                    {successMessageDelete && <p className="success-message">{successMessageDelete}</p>}
                    {errorMessageDelete && <p className="error-message">{errorMessageDelete}</p>}
                </form>
            </main>
            <Footer />
        </>
    );
}

export default ManageGenres;
