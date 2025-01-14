import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

const Footer = () => {
    const { t, i18n } = useTranslation();

    return (
        <footer id="footer">
            <p>&copy; 2025 Movies library. {t("All rights reserved")}.</p>
        </footer>
    )
};

export default Footer;