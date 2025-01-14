import { Link } from "react-router-dom";
import logo from "../img/movies-library-high-resolution-logo-transparent.png";
import { isAdmin, isUser, logout } from "../services/authService";
import { useEffect, useState } from "react";
import HeaderSearchFormComponent from "./HeaderSearchFormComponent";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'
import LanguageSwitcher from "./LanguageSwitcher";

const Header = () => {
    const { t, i18n } = useTranslation();
    const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem("accessToken"));

    useEffect(() => {
        const handleAuthStateChange = () => {
            setIsAuthenticated(!!localStorage.getItem("accessToken"));
        };

        // Listen for changes in auth state
        window.addEventListener("authStateChanged", handleAuthStateChange);
        return () => {
            window.removeEventListener("authStateChanged", handleAuthStateChange);
        };
    }, []);

    return (
        <header id="main-header">
            <div id="logo">
                <Link to="/">
                    <img src={logo} alt="Movies library logo" />
                </Link>
            </div>
            <nav id="main-menu">
                <ul>
                    <li><Link to="/">{t("Home")}</Link></li>

                    {isUser() && (
                        <li><Link to="/manage-your-ratings">{t("Manage Ratings")}</Link></li>
                    )}
                    {isAdmin() && (
                        <>
                            <li><Link to="/manage-movies">{t("Manage Movies")}</Link></li>
                            <li><Link to="/manage-actors">{t("Manage Actors")}</Link></li>
                            <li><Link to="/manage-genres">{t("Manage Genres")}</Link></li>
                        </>
                    )}
                    {!isAuthenticated && (
                        <>
                            <li><Link to="/login">{t("Login")}</Link></li>
                            <li><Link to="/register">{t("Register")}</Link></li>
                        </>
                    )}
                    {isAuthenticated && (
                        <li>
                            <Link to="/" onClick={logout}>
                                {t("Log out")}
                            </Link>
                        </li>
                    )}
                </ul>
            </nav>
            <HeaderSearchFormComponent />
            <LanguageSwitcher />
            <div className="clearfix"></div>
        </header>
    );
};

export default Header;
