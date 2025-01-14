import React from "react";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function ActorCard({ actor }) {
    const { t, i18n } = useTranslation();

    return (
        <div className="movie-cast-member">
            <h3 className="actor-name">{actor.name}</h3>
            <p className="actor-gender"><span>{t("Gender")}:</span> {t(actor.genderName)}</p>
            <p className="actor-birth-date"><span>{t("Birthdate")}:</span> {actor.birthDate}</p>
            <p className="actor-death-date"><span>{t("Death date")}:</span> {actor.deathDate && actor.deathDate !== "0001-01-01" ? actor.deathDate : "-"}</p>
            <p className="actor-role"><span>{t("Role")}:</span> {t(actor.roleName)}</p>
            <p className="actor-biography">{actor.biography}</p>
        </div>
    );
}

export default ActorCard;
