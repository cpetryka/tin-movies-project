import React from "react";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function capitalizeEachWord(title) {
    return title
        .split(" ")
        .map((word) => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
        .join(" ");
}

function MovieInfo({ movie }) {
    const { t, i18n } = useTranslation();

    return (
        <div className="movie-details">
            <h2 className="title">{capitalizeEachWord(movie.title)}</h2>
            <p className="release"><span>{t("Release date")}:</span> {movie.releaseDate}</p>
            <p className="director"><span>{t("Director")}:</span> {capitalizeEachWord(movie.director)}</p>
            <p className="genres">
                <span>{t("Genres")}:</span> {movie.genres.map((genre) => capitalizeEachWord(genre.name)).join(", ")}
            </p>
            <p className="rating">
                <span>{t("Average rating")}:</span> {movie.averageRating.toFixed(2)} <ion-icon name="star" className="standard-icon-size"></ion-icon>
            </p>
        </div>
    );
}

export default MovieInfo;
