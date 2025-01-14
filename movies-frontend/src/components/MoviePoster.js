import React from "react";

function MoviePoster({ posterUrl, title }) {
    return (
        <div className="movie-poster">
            <img src={posterUrl} alt={`${title} poster`} />
        </div>
    );
}

export default MoviePoster;
