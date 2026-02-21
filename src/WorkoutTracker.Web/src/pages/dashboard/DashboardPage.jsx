import { useAuth } from '../../context/AuthContext';
import { useNavigate } from 'react-router-dom';
import './dashboard.css';

export default function DashboardPage() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  function handleLogout() {
    logout();
    navigate('/login');
  }

  return (
    <div className="dashboard-container">
      <header className="dashboard-header">
        <h1>WorkoutTracker</h1>
        <div className="dashboard-header-right">
          <span>Hi, {user?.firstName}</span>
          <button onClick={handleLogout} className="dashboard-logout-btn">Logout</button>
        </div>
      </header>
      <main className="dashboard-main">
        <p>Dashboard — workouts coming soon.</p>
      </main>
    </div>
  );
}
