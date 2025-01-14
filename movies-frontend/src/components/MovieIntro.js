import React from "react";
import { IonIcon } from "@ionic/react";
import { star } from "ionicons/icons";
import {Link} from "react-router-dom";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'


function capitalizeEachWord(title) {
    return title
        .split(" ")
        .map((word) => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
        .join(" ");
}

function MovieIntro({ movie }) {
    const { t, i18n } = useTranslation();

    return (
        <div className="movie-box">
            <div className="movie-box-poster">
                <img
                    src={movie.posterUrl}
                    alt={`${movie.title} poster`}
                />
            </div>
            <div className="movie-box-details">
                <h2 className="title">{capitalizeEachWord(movie.title)}</h2>
                <p className="release">{movie.releaseDate}</p>
                <p className="rating">{movie.averageRating.toFixed(2)}{" "}<IonIcon icon={star} className="standard-icon-size" /></p>
                <Link to={`/movies/${movie.tmdbId}`} className="read-more">
                    {t("Read more")}
                </Link>
            </div>
        </div>
    );
}

export default MovieIntro;
