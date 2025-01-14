import {jwtDecode} from "jwt-decode";

const BASE_URL = "http://localhost:5249/api/security";

export const register = async (userData) => {
    const queryParams = new URLSearchParams(userData).toString();
    const response = await fetch(`${BASE_URL}/register?${queryParams}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
    });

    if (!response.ok) {
        throw new Error("Registration failed");
    }
    return await response.json();
};

export const login = async (userData) => {
    const queryParams = new URLSearchParams(userData).toString();
    const response = await fetch(`${BASE_URL}/login?${queryParams}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
    });

    if (!response.ok) {
        throw new Error("Login failed");
    }

    const data = await response.json();
    localStorage.setItem("accessToken", data.accessToken);
    localStorage.setItem("refreshToken", data.refreshToken);

    window.dispatchEvent(new CustomEvent("authStateChanged"));
    return data;
};

export const refresh = async () => {
    const refreshToken = localStorage.getItem("refreshToken");
    const queryParams = new URLSearchParams({ refreshToken }).toString();
    const response = await fetch(`${BASE_URL}/refresh?${queryParams}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
    });

    if (!response.ok) {
        throw new Error("Token refresh failed");
    }

    const data = await response.json();
    localStorage.setItem("accessToken", data.accessToken);
    localStorage.setItem("refreshToken", data.refreshToken);

    return data;
};

export const logout = () => {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");

    window.dispatchEvent(new CustomEvent("authStateChanged"));
};

export const fetchWithAuth = async (url, options = {}) => {
    const accessToken = localStorage.getItem("accessToken");
    options.headers = {
        ...options.headers,
        Authorization: `Bearer ${accessToken}`,
    };

    const response = await fetch(url, options);

    if (response.status === 401) {
        await refresh();
        options.headers.Authorization = `Bearer ${localStorage.getItem("accessToken")}`;
        return fetch(url, options);
    }

    return response;
};

export const getId = () => {
    const token = localStorage.getItem("accessToken");

    if (!token) return 0;

    try {
        const decodedToken = jwtDecode(token);
        return parseInt(decodedToken.nameid, 10);
    } catch (error) {
        console.error("Failed to decode token:", error);
        return 0;
    }
};

export const getRole = () => {
    const token = localStorage.getItem("accessToken");

    if (!token) return "";

    try {
        const decodedToken = jwtDecode(token);
        return decodedToken.role;
    } catch (error) {
        console.error("Failed to decode token:", error);
        return "";
    }
};

// Helper functions to check user role
export const isAdmin = () => getRole() === "Admin";
export const isUserOrAdmin = () => getRole() === "User" || isAdmin();
export const isUser = () => getRole() === "User";