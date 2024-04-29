import React from "react";
import './header.css'

let year = (new Date()).getFullYear();

function Header() {
    return (
        <header>
            <p>Copyright {year}</p>
        </header>
    );
}

export default Header;