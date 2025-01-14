import React, { useState, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './css/style.css';
import Home from "./components/Home";
import MovieDetails from "./components/MovieDetails";
import Login from "./components/Login";
import Register from "./components/Register";
import ProtectedRoute from "./components/ProtectedRoute";
import ManageGenres from "./components/ManageGenres";
import ManageActors from "./components/ManageActors";
import ManageMovies from "./components/ManageMovies";
import SearchPage from "./components/SearchPage";
import ManageYourRatings from "./components/ManageYourRatings";
import './config/i18n';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/movies/:tmdbId" element={<MovieDetails />} />
                <Route path="/search" element={<SearchPage />} />
                <Route
                    path="/manage-your-ratings"
                    element={
                        <ProtectedRoute>
                            <ManageYourRatings />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="/manage-movies"
                    element={
                        <ProtectedRoute>
                            <ManageMovies />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="/manage-actors"
                    element={
                        <ProtectedRoute>
                            <ManageActors />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="/manage-genres"
                    element={
                        <ProtectedRoute>
                            <ManageGenres />
                        </ProtectedRoute>
                    }
                />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
            </Routes>
        </Router>
    );
}

export default App;