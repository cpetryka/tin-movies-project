import React, { useState } from "react";
import {Link, useNavigate} from "react-router-dom";
import logo from "../img/movies-library-high-resolution-logo-transparent.png";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'
import {login} from "../services/authService";

const Login = () => {
    const { t, i18n } = useTranslation();
    const [formData, setFormData] = useState();
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await login(formData);
        /*try {
            await login(formData);
            alert("Login successful!");
        } catch (err) {
            setError(err.message);
        }*/
        navigate(`/`);
    };

    return (
        <div id="login-page">
            <div id="login-logo-wrapper">
                <Link to={"/"}>
                    <img src={logo} alt="Movies library logo"/>
                </Link>
            </div>
            <div id="login-wrapper">
                <div id="login">
                    <h1>{t("Login")}</h1>
                    <form onSubmit={handleSubmit}>
                        <input
                            type="email"
                            name="Email"
                            placeholder="E-mail"
                            onChange={handleChange}
                            required
                        />
                        <br/>
                        <input
                            type="password"
                            name="Password"
                            placeholder={t("Password")}
                            onChange={handleChange}
                            required
                        />
                        <br/>
                        <button type="submit">{t("Sign In")}</button>

                        {error && <p className={"form-error"}>{error}</p>}
                    </form>
                    <p>
                        {t("Don't have an account?")} <Link to="/register">{t("Register")}</Link>
                    </p>
                </div>
            </div>
        </div>
    );
};

export default Login;
