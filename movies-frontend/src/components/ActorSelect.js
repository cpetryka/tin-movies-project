import React, { useState, useEffect } from "react";
import {fetchWithAuth} from "../services/authService";


function ActorWithRoleSelect({ selectedActors, setSelectedActors }) {
    const [actors, setActors] = useState([]);

    useEffect(() => {
        const fetchActors = async () => {
            try {
                const response = await fetchWithAuth("http://localhost:5249/api/actors/get-all-actors");
                const data = await response.json();
                setActors(data);
                console.log(data);
            } catch (error) {
                console.error("Error fetching actors:", error);
            }
        };

        fetchActors();
    }, []);

    const handleChange = (e) => {
        const options = e.target.options;
        const selected = Array.from(options)
            .filter((option) => option.selected)
            .map((option) => {
                const { actor, role } = JSON.parse(option.value);
                return {
                    ...actor,
                    roleName: role
                };
            });

        setSelectedActors(selected);
    };

    return (
        <select id="actors" name="actors" size="6" multiple onChange={handleChange}>
            {actors.map((actor, index) => (
                <React.Fragment key={index}>
                    <option value={JSON.stringify({ actor, role: "Lead" })}>
                        {actor.name} - Lead
                    </option>
                    <option value={JSON.stringify({ actor, role: "Supporting" })}>
                        {actor.name} - Supporting
                    </option>
                </React.Fragment>
            ))}
        </select>
    );
}

export default ActorWithRoleSelect;
