import React, { useState, useEffect } from "react";
import Header from "./Header";
import Footer from "./Footer";
import GenderSelect from "./GenderSelect";
import { fetchWithAuth } from "../services/authService";
import { useTranslation } from "react-i18next";
import i18n from '../config/i18n'

function ManageActors() {
    const { t, i18n } = useTranslation();
    const [actorData, setActorData] = useState({
        name: "",
        genderId: 0,
        birthDate: "",
        deathDate: "",
        biography: "",
    });
    const [actors, setActors] = useState([]);
    const [selectedActorUpdate, setSelectedActorUpdate] = useState(null);
    const [selectedActorDelete, setSelectedActorDelete] = useState(null);
    const [editedActorData, setEditedActorData] = useState({
        name: "",
        genderId: 0,
        birthDate: "",
        deathDate: "",
        biography: "",
    });
    const [loading, setLoading] = useState(false);
    const [successMessageAdd, setSuccessMessageAdd] = useState("");
    const [successMessageEdit, setSuccessMessageEdit] = useState("");
    const [successMessageDelete, setSuccessMessageDelete] = useState("");
    const [errorMessageAdd, setErrorMessageAdd] = useState("");
    const [errorMessageEdit, setErrorMessageEdit] = useState("");
    const [errorMessageDelete, setErrorMessageDelete] = useState("");

    useEffect(() => {
        const fetchActors = async () => {
            try {
                const response = await fetchWithAuth("http://localhost:5249/api/actors/get-all-actors");
                if (response.ok) {
                    const data = await response.json();
                    setActors(data);
                } else {
                    throw new Error("Failed to fetch actors.");
                }
            } catch (error) {
                setErrorMessageEdit(t("An error occurred while fetching actors."));
                setErrorMessageDelete(t("An error occurred while fetching actors."));
            }
        };

        fetchActors();
    }, []);

    const handleInputChange = (e, setData, data) => {
        const { name, value } = e.target;
        setData({ ...data, [name]: value });
    };

    const handleAddActor = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageAdd("");
        setErrorMessageAdd("");

        const payload = {
            ...actorData,
            genderId: parseInt(actorData.genderId, 10),
            birthDate: new Date(actorData.birthDate),
            deathDate: actorData.deathDate ? new Date(actorData.deathDate) : "0001-01-01",
        };

        try {
            const response = await fetchWithAuth("http://localhost:5249/api/actors/add-new-actor", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                setSuccessMessageAdd("Actor added successfully!");
                setActorData({ name: "", genderId: -1, birthDate: "", deathDate: "", biography: "" });
                const updatedActors = await fetchWithAuth("http://localhost:5249/api/actors/get-all-actors");
                setActors(await updatedActors.json());
            } else {
                setErrorMessageAdd(t("Failed to add actor. Please check the input data."));
            }
        } catch (error) {
            setErrorMessageAdd(t("An error occurred while adding the actor."));
        } finally {
            setLoading(false);
        }
    };

    const handleEditActor = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageEdit("");
        setErrorMessageEdit("");

        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/actors/update-actor-by-id?actorId=${selectedActorUpdate.id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(editedActorData),
            });

            if (response.ok) {
                setSuccessMessageEdit(t("Actor updated successfully!"));
                setEditedActorData({ name: "", genderId: -1, birthDate: "", deathDate: "", biography: "" });
                setSelectedActorUpdate(null);
                const updatedActors = await fetchWithAuth("http://localhost:5249/api/actors/get-all-actors");
                setActors(await updatedActors.json());
            } else {
                setErrorMessageEdit(t("Failed to update actor."));
            }
        } catch (error) {
            setErrorMessageEdit(t("An error occurred while updating the actor."));
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteActor = async (e) => {
        e.preventDefault();
        setLoading(true);
        setSuccessMessageDelete("");
        setErrorMessageDelete("");

        try {
            const response = await fetchWithAuth(`http://localhost:5249/api/actors/delete-actor-by-id?actorId=${selectedActorDelete.id}`, {
                method: "DELETE",
            });

            if (response.ok) {
                setSuccessMessageDelete(t("Actor deleted successfully!"));
                setSelectedActorDelete(null);
                const updatedActors = await fetchWithAuth("http://localhost:5249/api/actors/get-all-actors");
                setActors(await updatedActors.json());
            } else {
                setErrorMessageDelete(t("Failed to delete actor."));
            }
        } catch (error) {
            setErrorMessageDelete(t("An error occurred while deleting the actor."));
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Header />
            <main id="main-section">
                <h1>{t("Add New Actor")}</h1>
                <form onSubmit={handleAddActor} id="add-new-entity-form">
                    <label htmlFor="name">{t("Name")}:</label>
                    <input
                        type="text"
                        name="name"
                        id="name"
                        value={actorData.name}
                        onChange={(e) => handleInputChange(e, setActorData, actorData)}
                        required
                    />
                    <br />
                    <label htmlFor="gender">{t("Gender")}:</label>
                    <GenderSelect
                        selectedGender={actorData.genderId}
                        setSelectedGender={(genderId) =>
                            setActorData({ ...actorData, genderId })
                        }
                    />
                    <br />
                    <label htmlFor="birthDate">{t("Birthdate")}:</label>
                    <input
                        type="date"
                        name="birthDate"
                        id="birthDate"
                        value={actorData.birthDate}
                        onChange={(e) => handleInputChange(e, setActorData, actorData)}
                    />
                    <br />
                    <label htmlFor="deathDate">{t("Death Date (you can leave this field empty)")}:</label>
                    <input
                        type="date"
                        name="deathDate"
                        id="deathDate"
                        value={actorData.deathDate}
                        onChange={(e) => handleInputChange(e, setActorData, actorData)}
                    />
                    <br />
                    <label htmlFor="biography">{t("Biography")}:</label>
                    <textarea
                        name="biography"
                        id="biography"
                        value={actorData.biography}
                        onChange={(e) => handleInputChange(e, setActorData, actorData)}
                    ></textarea>
                    <br />
                    <button type="submit" disabled={loading}>
                        {loading ? t("Adding...") : t("Add Actor")}
                    </button>
                    {successMessageAdd && <p className="success-message">{successMessageAdd}</p>}
                    {errorMessageAdd && <p className="error-message">{errorMessageAdd}</p>}
                </form>

                <div className="separator"></div>

                <h1>{t("Update Selected Actor")}</h1>
                <form onSubmit={handleEditActor} id="add-new-entity-form">
                    <label htmlFor="selectedActor">{t("Select Actor")}:</label>
                    <select
                        name="selectedActor"
                        id="selectedActor"
                        value={selectedActorUpdate?.id || ""}
                        onChange={(e) => {
                            const actor = actors.find((a) => a.id === parseInt(e.target.value, 10));
                            setSelectedActorUpdate(actor);
                            setEditedActorData(actor || { name: "", genderId: 0, birthDate: "", deathDate: "", biography: "" });
                        }}
                    >
                        <option value="" disabled>
                            {t("-- Select Actor --")}
                        </option>
                        {actors.map((actor) => (
                            <option key={actor.id} value={actor.id}>
                                {actor.name}
                            </option>
                        ))}
                    </select>

                    {selectedActorUpdate && (
                        <>
                            <br/>
                            <label htmlFor="name">{t("Name")}:</label>
                            <input
                                type="text"
                                name="name"
                                id="name"
                                value={editedActorData.name}
                                onChange={(e) => handleInputChange(e, setEditedActorData, editedActorData)}
                            />
                            <br/>
                            <label htmlFor="gender">{t("Gender")}:</label>
                            <GenderSelect
                                selectedGender={editedActorData.genderId}
                                setSelectedGender={(genderId) =>
                                    setEditedActorData({...editedActorData, genderId})
                                }
                            />
                            <br/>
                            <label htmlFor="birthDate">{t("Birthdate")}:</label>
                            <input
                                type="date"
                                name="birthDate"
                                id="birthDate"
                                value={editedActorData.birthDate}
                                onChange={(e) => handleInputChange(e, setEditedActorData, editedActorData)}
                            />
                            <br/>
                            <label htmlFor="deathDate">{t("Death Date (you can leave this field empty)")}:</label>
                            <input
                                type="date"
                                name="deathDate"
                                id="deathDate"
                                value={editedActorData.deathDate}
                                onChange={(e) => handleInputChange(e, setEditedActorData, editedActorData)}
                            />
                            <br/>
                            <label htmlFor="biography">{t("Biography")}:</label>
                            <textarea
                                name="biography"
                                id="biography"
                                value={editedActorData.biography}
                                onChange={(e) => handleInputChange(e, setEditedActorData, editedActorData)}
                            ></textarea>
                            <br/>
                            <button type="submit" disabled={loading}>
                                {loading ? t("Updating...") : t("Update Actor")}
                            </button>
                        </>
                    )}

                    {successMessageEdit && <p className="success-message">{successMessageEdit}</p>}
                    {errorMessageEdit && <p className="error-message">{errorMessageEdit}</p>}
                </form>

                <div className="separator"></div>

                <h1>{t("Delete Selected Actor")}</h1>
                <form onSubmit={handleDeleteActor} id="add-new-entity-form">
                    <label htmlFor="selectedActorDelete">{t("Select Actor")}:</label>
                    <select
                        name="selectedActorDelete"
                        id="selectedActorDelete"
                        value={selectedActorDelete?.id || ""}
                        onChange={(e) => {
                            const actor = actors.find((a) => a.id === parseInt(e.target.value, 10));
                            setSelectedActorDelete(actor);
                        }}
                    >
                        <option value="" disabled>
                            {t("-- Select Actor --")}
                        </option>
                        {actors.map((actor) => (
                            <option key={actor.id} value={actor.id}>
                                {actor.name}
                            </option>
                        ))}
                    </select>
                    <br/>

                    {selectedActorDelete && (
                        <button type="submit" disabled={loading}>
                            {loading ? t("Deleting...") : t("Delete Actor")}
                        </button>
                    )}

                    {successMessageDelete && <p className="success-message">{successMessageDelete}</p>}
                    {errorMessageDelete && <p className="error-message">{errorMessageDelete}</p>}
                </form>

            </main>
            <Footer/>
        </>
    );
}

export default ManageActors;
