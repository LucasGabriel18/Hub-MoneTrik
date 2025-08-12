import { CircleDollarSign, CircleGauge, Plus, Home } from "lucide-react";
import { useNavigate, useLocation } from "react-router-dom";
import "./navbar.css";

function Navbar() {
  const navegar = useNavigate();
  const location = useLocation();

  const navegarPara = (rota: string) => {
    navegar(rota);
  };

  return (
    <nav className="navbar">
      <div className="navbar-container">
        <div className="navbar-logo">
          <CircleDollarSign
            className="logo-icon"
            size={24}
            strokeWidth={2.25}
          />
          <span className="logo-text">Hub Monetrik</span>
        </div>

        <div className="navbar-links">
          <button
            className={`nav-button ${
              location.pathname === "/" ? "active" : ""
            }`}
            onClick={() => navegarPara("/")}
          >
            <Home className="nav-icon" size={18} />
            Home
          </button>

          <button
            className={`nav-button ${
              location.pathname === "/dashboard" ? "active" : ""
            }`}
            onClick={() => navegarPara("/dashboard")}
          >
            <CircleGauge className="nav-icon" size={18} strokeWidth={2.25} />
            Dashboard
          </button>

          <button
            className="nav-button"
            onClick={() => navegarPara("/cadastrar-despesa")}
          >
            <Plus className="nav-icon" size={18} />
            Nova Despesa
          </button>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
