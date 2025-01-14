import React, { useState, useEffect } from "react";
import Header from "./Header";
import Footer from "./Footer";
import {fetchWithAuth, getId} from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function capitalizeEachWord(title) {
    return title
        .split(" ")
        .map((word) => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
        .join(" ");
}

function ManageYourRatings() {
    const { t, i18n } = useTranslation();
    const [ratings, setRatings] = useState([]);
    const [selectedRatingUpdate, setSelectedRatingUpdate] = useState(null);
    const [selectedRatingDelete, setSelectedRatingDelete] = useState(null);
    const [editedRating, setEditedRating] = useState({
        starsNumber: 0,
        movieId: 0,
    });
    const [loading, setLoading] = useState(false);
    const [successMessageEdit, setSuccessMessageEdit] = useState("");
    const [successMessageDelete, setSuccessMessageDelete] = useState("");
    const [errorMessageEdit, setErrorMessageEdit] = useState("");
    const [errorMessageDelete, setErrorMessageDelete] = useState("");


    useEffect(() => {
        const fetchRatings = async () => {
            try {
                const userId = getId();
                const response = await fetchWithAuth(
                    `http://localhost:5249/api/movies/get-all-movie-ratings-added-by?userId=${userId}`
                );
                if (response.ok) {
                    const data = await response.json();
                    setRatings(data);
                } else {
                    throw new Error(t("Failed to fetch ratings."));
                }
            } catch (error) {
                setErrorMessageEdit(t("An error occurred while fetching ratings."));
                setErrorMessageDelete(t("An error occurred while fetching ratings."));
            }
        };

        fetchRatings();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setEditedRating({ ...editedRating, [name]: value });
    };

    const handleEditRating = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageEdit("");
        setErrorMessageEdit("");

        try {
            const userId = getId();
            const response = await fetchWithAuth(
                `http://localhost:5249/api/movies/update-movie-rating-added-by?movieId=${selectedRatingUpdate.movieId}&userId=${userId}&newRatingId=${editedRating.starsNumber}`,
                {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                }
            );

            if (response.ok) {
                setSuccessMessageEdit(t("Rating updated successfully!"));
                setSelectedRatingUpdate(null);
                const updatedRatings = await fetchWithAuth(
                    `http://localhost:5249/api/movies/get-all-movie-ratings-added-by?userId=${userId}`
                );
                setRatings(await updatedRatings.json());
            } else {
                setErrorMessageEdit(t("Failed to update rating."));
            }
        } catch (error) {
            setErrorMessageEdit(t("An error occurred while updating ratings."));
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteRating = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageDelete("");
        setErrorMessageDelete("");

        try {
            const userId = getId();
            const response = await fetchWithAuth(
                `http://localhost:5249/api/movies/delete-movie-rating-added-by?userId=${userId}&movieId=${selectedRatingDelete.movieId}`,
                {
                    method: "DELETE",
                }
            );

            if (response.ok) {
                setSuccessMessageDelete(t("Rating deleted successfully!"));
                setSelectedRatingDelete(null);
                const updatedRatings = await fetchWithAuth(
                    `http://localhost:5249/api/movies/get-all-movie-ratings-added-by?userId=${userId}`
                );
                setRatings(await updatedRatings.json());
            } else {
                setErrorMessageDelete(t("Failed to delete rating."));
            }
        } catch (error) {
            setErrorMessageDelete(t("An error occurred while deleting the rating."));
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Header />
            <main id="main-section">
                <h1>{t("Update Your Ratings")}</h1>

                {/* Edit Rating Form */}
                <form onSubmit={handleEditRating} id="add-new-entity-form">
                    <label htmlFor="selectedRating">{t("Select Rating")}:</label>
                    <select
                        name="selectedRating"
                        id="selectedRating"
                        value={selectedRatingUpdate?.movieId || ""}
                        onChange={(e) => {
                            const rating = ratings.find((r) => r.movieId === parseInt(e.target.value, 10));
                            setSelectedRatingUpdate(rating);
                            setEditedRating(rating || {starsNumber: 0, movieId: 0});
                        }}
                    >
                        <option value="" disabled>
                            -- {t("Select Rating")} --
                        </option>
                        {ratings.map((rating) => (
                            <option key={rating.movieRatingId} value={rating.movieId}>
                                {capitalizeEachWord(rating.movieTitle)} -> {rating.starsNumber} {t("star(s)")}
                            </option>
                        ))}
                    </select>
                    <br/>

                    {selectedRatingUpdate && (
                        <>
                            <label htmlFor="starsNumber">{t("Stars")} (1-5):</label>
                            <input
                                type="number"
                                name="starsNumber"
                                id="starsNumber"
                                value={editedRating.starsNumber}
                                onChange={handleInputChange}
                                min="1"
                                max="5"
                                required
                            />
                            <br/>
                            <button type="submit" disabled={loading}>
                                {loading ? t("Updating") + "..." : t("Update Rating")}
                            </button>
                        </>
                    )}

                    {successMessageEdit && <p className="success-message">{successMessageEdit}</p>}
                    {errorMessageEdit && <p className="error-message">{errorMessageEdit}</p>}
                </form>

                <div className="separator"></div>
                <h1>{t("Delete Your Ratings")}</h1>

                {/* Delete Rating Form */}
                <form onSubmit={handleDeleteRating} id="add-new-entity-form">
                    <label htmlFor="selectedRatingDelete">{t("Select Rating")}:</label>
                    <select
                        name="selectedRatingDelete"
                        id="selectedRatingDelete"
                        value={selectedRatingDelete?.movieId || ""}
                        onChange={(e) => {
                            const rating = ratings.find((r) => r.movieId === parseInt(e.target.value, 10));
                            setSelectedRatingDelete(rating);
                        }}
                    >
                        <option value="" disabled>
                            -- {t("Select Rating")} --
                        </option>
                        {ratings.map((rating) => (
                            <option key={rating.movieRatingId} value={rating.movieId}>
                                {capitalizeEachWord(rating.movieTitle)} -> {rating.starsNumber} {t("star(s)")}
                            </option>
                        ))}
                    </select>
                    <br/>

                    {selectedRatingDelete && (
                        <button type="submit" disabled={loading}>
                            {loading ? t("Deleting") + "..." : t("Delete Rating")}
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

export default ManageYourRatings;
