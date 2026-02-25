import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { register } from '../../api/authApi';
import { useAuth } from '../../context/AuthContext';
import './auth.css';

export default function RegisterPage() {
  const [form, setForm] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    gender: '0',
  });
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const { saveAuth } = useAuth();
  const navigate = useNavigate();

  function handleChange(e) {
    setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');
    setLoading(true);
    try {
      const { data } = await register({ ...form, gender: parseInt(form.gender) });
      saveAuth(data);
      navigate('/dashboard');
    } catch (err) {
      setError(err.response?.data?.message || 'Registration failed. Please try again.');
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className="auth-container">
      <div className="auth-card">
        <h1 className="auth-title">WorkoutTracker</h1>
        <h2 className="auth-subtitle">Kreiranje naloga</h2>
        <form onSubmit={handleSubmit} className="auth-form">
          <div className="auth-row">
            <div className="auth-field">
              <label htmlFor="firstName">Ime</label>
              <input
                id="firstName"
                name="firstName"
                type="text"
                value={form.firstName}
                onChange={handleChange}
                required
              />
            </div>
            <div className="auth-field">
              <label htmlFor="lastName">Prezime</label>
              <input
                id="lastName"
                name="lastName"
                type="text"
                value={form.lastName}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          <div className="auth-field">
            <label htmlFor="email">Email</label>
            <input
              id="email"
              name="email"
              type="email"
              value={form.email}
              onChange={handleChange}
              required
              autoComplete="email"
            />
          </div>
          <div className="auth-field">
            <label htmlFor="password">Lozinka</label>
            <input
              id="password"
              name="password"
              type="password"
              value={form.password}
              onChange={handleChange}
              required
              autoComplete="new-password"
            />
          </div>
          <div className="auth-field">
            <label htmlFor="gender">Pol</label>
            <select id="gender" name="gender" value={form.gender} onChange={handleChange}>
              <option value="0">Muški</option>
              <option value="1">Ženski</option>
            </select>
          </div>
          {error && <p className="auth-error">{error}</p>}
          <button type="submit" className="auth-btn" disabled={loading}>
            {loading ? 'Kreiranje naloga...' : 'Registruj se'}
          </button>
        </form>
        <p className="auth-link">
          Već imate nalog? <Link to="/login">Prijavite se</Link>
        </p>
      </div>
    </div>
  );
}
