import React, { useState, useEffect } from "react";
import { fetchWithAuth } from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function GenderSelect({ selectedGender, setSelectedGender }) {
    const { t, i18n } = useTranslation();
    const [genders, setGenders] = useState([]);

    useEffect(() => {
        const fetchGenders = async () => {
            try {
                const response = await fetchWithAuth("http://localhost:5249/api/actors/get-all-genders");
                const data = await response.json();
                console.log(data);
                setGenders(data);
            } catch (error) {
                console.error("Error fetching genders:", error);
            }
        };

        fetchGenders();
    }, []);

    return (
        <select
            id="gender"
            name="genderId"
            value={selectedGender}
            onChange={(e) => setSelectedGender(e.target.value)}
            required
        >
            <option value="0" disabled>
                -- {t("Select Gender")} --
            </option>
            {genders.map((gender, index) => (
                <option key={index} value={index + 1}>
                    {t(gender.name)}
                </option>
            ))}
        </select>
    );
}

export default GenderSelect;
