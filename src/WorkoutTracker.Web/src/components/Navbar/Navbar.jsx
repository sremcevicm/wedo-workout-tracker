import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import './Navbar.css';

export default function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  function handleLogout() {
    logout();
    navigate('/login');
  }

  return (
    <header className="navbar">
      <div className="navbar-brand">
        <img src="/wedo-logo.svg" alt="WEDO logo" className="navbar-logo" />
        <span className="navbar-title">WEDO - Workout tracker</span>
      </div>
      <div className="navbar-right">
        <span className="navbar-greeting">Zdravo, <strong>{user?.firstName}</strong></span>
        <button onClick={handleLogout} className="navbar-logout-btn">Odjavi se</button>
      </div>
    </header>
  );
}
