import React, { useEffect, useState } from "react";
import {star} from "ionicons/icons";
import {IonIcon} from "@ionic/react";
import {fetchWithAuth, getId, isUser} from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function MovieReviews({ movieId }) {
    const { t, i18n } = useTranslation();
    const [ratings, setRatings] = useState([]);
    const [userRating, setUserRating] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchRatings = async () => {
            try {
                const response = await fetchWithAuth(`http://localhost:5249/api/movies/get-movie-ratings?movieId=${movieId}`);
                console.log(movieId)
                console.log(response)
                if (!response.ok) {
                    throw new Error(t("Failed to fetch ratings!"));
                }
                const data = await response.json();
                setRatings(data);
            } catch (err) {
                setError(err.message);
            }
        };

        fetchRatings();
    }, [movieId]);

    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem("accessToken"));

    useEffect(() => {
        const handleAuthStateChange = () => {
            setIsAuthenticated(!!localStorage.getItem("accessToken"));
        };

        window.addEventListener("authStateChanged", handleAuthStateChange);
        return () => {
            window.removeEventListener("authStateChanged", handleAuthStateChange);
        };
    }, []);

    const handleRatingSubmit = async (e) => {
        e.preventDefault();
        if (!userRating) {
            return alert("Please select a rating");
        }

        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/movies/add-movie-rating?movieId=${movieId}&ratingId=${userRating}&userId=${getId()}`, {
                method: "POST",
            });
            if (!response.ok) {
                throw new Error(t("Failed to submit rating! You can only add one review for each video."));
            }
            const newRating = parseInt(userRating, 10);
            setRatings((prev) => [...prev, { starsNumber: newRating }]);
            setUserRating(null);
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <>
            <h2>{t("Reviews")}</h2>

            <div id="reviews">
                {ratings.map((rating, index) => (
                    <div key={index} className="movie-review">
                        {rating.starsNumber} <IonIcon icon={star} className="standard-icon-size" />
                    </div>
                ))}
            </div>

            <div id="add-new-review">
                <h3>{t("Add your own review")}:</h3>
                {isUser() && (
                    <>
                        <form onSubmit={handleRatingSubmit}>
                            {[1, 2, 3, 4, 5].map((starNumbers) => (
                                <div>
                                    <input
                                        type="radio"
                                        id={`${starNumbers}-stars`}
                                        name="stars-number"
                                        value={starNumbers}
                                        checked={userRating === String(starNumbers)}
                                        onChange={(e) => setUserRating(e.target.value)}
                                    />
                                    <label htmlFor={`${starNumbers}-stars`}>
                                        {[...Array(starNumbers)].map((_, index) => (
                                            <><IonIcon icon={star} className="standard-icon-size" /></>
                                        ))}
                                    </label>
                                </div>
                            ))}
                            <button type="submit">Submit</button>
                        </form>
                        {error && <p className={"form-error"}>{error}</p>}
                    </>
                )}
                {!isUser() && (
                    <><p>{t("You need to be logged in as a user to add a review!")}</p></>
                )}
            </div>
        </>
    );
}

export default MovieReviews;
