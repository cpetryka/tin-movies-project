import React, { useState } from "react";
import { Link } from "react-router-dom";
import logo from "../img/movies-library-high-resolution-logo-transparent.png";
import {register} from "../services/authService";
import {useTranslation} from "react-i18next";
import i18n from '../config/i18n'

const Register = () => {
    const { t, i18n } = useTranslation();
    const [formData, setFormData] = useState({});
    const [error, setError] = useState(null);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await register(formData);
            alert("Registration successful!");
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div id="register-page">
            <div id="login-logo-wrapper">
                <Link to={"/"}>
                    <img src={logo} alt="Movies library logo" />
                </Link>
            </div>
            <div id="login-wrapper">
                <div id="login">
                    <h1>{t("Register")}</h1>
                    <form onSubmit={handleSubmit}>
                        <input
                            type="text"
                            name="Name"
                            placeholder={t("Name")}
                            onChange={handleChange}
                            required
                        />
                        <br />
                        <input
                            type="email"
                            name="Email"
                            placeholder="E-mail"
                            onChange={handleChange}
                            required
                        />
                        <br />
                        <input
                            type="password"
                            name="Password"
                            placeholder={t("Password")}
                            onChange={handleChange}
                            required
                        />
                        <br />
                        <button type="submit">{t("Sign Up")}</button>

                        {error && <p className={"form-error"}>{error}</p>}
                    </form>
                    <p>
                        {t("Already have an account?")} <Link to="/login">{t("Login")}</Link>
                    </p>
                </div>
            </div>
        </div>
    );
};

export default Register;
