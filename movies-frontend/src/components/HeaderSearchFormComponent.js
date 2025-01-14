import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

const HeaderSearchFormComponent = () => {
    const { t, i18n } = useTranslation();
    const [query, setQuery] = useState('');
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();
        navigate(`/search?query=${encodeURIComponent(query)}`);
    };

    return (
        <form onSubmit={handleSubmit} id="header-search">
            <input
                type="text"
                name="search"
                placeholder={t("Search for movies")}
                value={query}
                onChange={(e) => setQuery(e.target.value)}
                required
            />
            <button type="submit">{t("Search")}</button>
        </form>
    );
};

export default HeaderSearchFormComponent;