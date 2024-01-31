import { useEffect, useState } from 'react';
import './App.css';
import RadPdf from './RadPdf.jsx';

function App() {

    return (
        <div>
            <h1 id="tabelLabel">RAD PDF React Demo</h1>
            <p>This component demonstrates how RAD PDF can be used with React.</p>
            <RadPdf />
        </div>
    );
}

export default App;