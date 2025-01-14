import React from "react";
import ActorCard from "./ActorCard";

function MovieCast({ actors }) {
    return (
        <div id="movie-cast">
            {actors.map((actor, index) => (
                <ActorCard key={index} actor={actor} />
            ))}
        </div>
    );
}

export default MovieCast;
