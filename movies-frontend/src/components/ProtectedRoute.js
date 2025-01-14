import React from "react";
import { Navigate } from "react-router-dom";
import { isAdmin, isUser } from "../services/authService";

const ProtectedRoute = ({ children, role }) => {
    if (role === "Admin" && !isAdmin()) return <Navigate to="/unauthorized" />;
    if (role === "User" && !isUser()) return <Navigate to="/unauthorized" />;
    return children;
};

export default ProtectedRoute;
