import i18n from "../config/i18n";

const LanguageSwitcher = () => {
    const changeLanguage = (lng) => {
        i18n.changeLanguage(lng);
    };

    return (
        <div className={"languageSwitcher"}>
            <button onClick={() => changeLanguage('en')}>EN</button>
            <button onClick={() => changeLanguage('pl')}>PL</button>
        </div>
    );
};

export default LanguageSwitcher;