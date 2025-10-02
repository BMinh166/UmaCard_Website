import React from 'react';
import { Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import UmaPages from './pages/UmaPages';
import AddCard from './components/AddCard';

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/card/:id" element={<UmaPages />} />
      <Route path="/add" element={<AddCard />} />
    </Routes>
  );
}

export default App;