import { useEffect, useState } from 'react';
import { getWorkouts, createWorkout, getMonthlyProgress } from '../../api/workoutsApi';
import './dashboard.css';

const EXERCISE_TYPES = [
  { value: 0, label: 'Cardio' },
  { value: 1, label: 'Strength' },
  { value: 2, label: 'Flexibility' },
];

const EXERCISE_LABELS = { 0: 'Cardio', 1: 'Strength', 2: 'Flexibility' };

const today = new Date().toISOString().split('T')[0];

const EMPTY_FORM = {
  exerciseType: 0,
  durationInMinutes: '',
  caloriesBurned: '',
  difficulty: 5,
  fatigue: 5,
  workoutDate: today,
  notes: '',
};

export default function DashboardPage() {

  const [workouts, setWorkouts] = useState([]);
  const [form, setForm] = useState(EMPTY_FORM);
  const [formError, setFormError] = useState('');
  const [formLoading, setFormLoading] = useState(false);

  const now = new Date();
  const [progressYear, setProgressYear] = useState(now.getFullYear());
  const [progressMonth, setProgressMonth] = useState(now.getMonth() + 1);
  const [progress, setProgress] = useState([]);
  const [progressLoading, setProgressLoading] = useState(false);

  useEffect(() => {
    getWorkouts().then(setWorkouts).catch(console.error);
  }, []);

  useEffect(() => {
    setProgressLoading(true);
    getMonthlyProgress(progressYear, progressMonth)
      .then(setProgress)
      .catch(console.error)
      .finally(() => setProgressLoading(false));
  }, [progressYear, progressMonth]);

  function handleChange(e) {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setFormError('');
    if (!form.durationInMinutes || !form.caloriesBurned) {
      setFormError('Trajanje i kalorije su obavezni.');
      return;
    }
    setFormLoading(true);
    try {
      await createWorkout({
        exerciseType: Number(form.exerciseType),
        durationInMinutes: Number(form.durationInMinutes),
        caloriesBurned: Number(form.caloriesBurned),
        difficulty: Number(form.difficulty),
        fatigue: Number(form.fatigue),
        workoutDate: new Date(form.workoutDate).toISOString(),
        notes: form.notes || null,
      });
      setForm(EMPTY_FORM);
      const updated = await getWorkouts();
      setWorkouts(updated);
      const updatedProgress = await getMonthlyProgress(progressYear, progressMonth);
      setProgress(updatedProgress);
    } catch {
      setFormError('Greška pri dodavanju treninga.');
    } finally {
      setFormLoading(false);
    }
  }

  // Summary stats from all workouts
  const totalWorkouts = workouts.length;
  const totalMinutes = workouts.reduce((s, w) => s + w.durationInMinutes, 0);
  const avgDifficulty = totalWorkouts
    ? (workouts.reduce((s, w) => s + w.difficulty, 0) / totalWorkouts).toFixed(1)
    : '—';
  const avgFatigue = totalWorkouts
    ? (workouts.reduce((s, w) => s + w.fatigue, 0) / totalWorkouts).toFixed(1)
    : '—';

  const MONTHS = [
    'Januar', 'Februar', 'Mart', 'April', 'Maj', 'Jun',
    'Jul', 'Avgust', 'Septembar', 'Oktobar', 'Novembar', 'Decembar',
  ];

  return (
    <main className="dashboard-main">

        {/* SUMMARY CARDS */}
        <section className="summary-cards">
          <div className="summary-card">
            <span className="summary-label">Ukupno treninga</span>
            <span className="summary-value">{totalWorkouts}</span>
          </div>
          <div className="summary-card">
            <span className="summary-label">Ukupno minuta</span>
            <span className="summary-value">{totalMinutes}</span>
          </div>
          <div className="summary-card">
            <span className="summary-label">Prosečna težina</span>
            <span className="summary-value">{avgDifficulty}</span>
          </div>
          <div className="summary-card">
            <span className="summary-label">Prosečan zamor</span>
            <span className="summary-value">{avgFatigue}</span>
          </div>
        </section>

        {/* TWO COLUMNS */}
        <section className="dashboard-columns">

          {/* LEFT — ADD WORKOUT */}
          <div className="dashboard-col">
            <h2 className="col-title">Dodaj trening</h2>
            <form className="workout-form" onSubmit={handleSubmit}>
              <div className="form-group">
                <label>Tip vežbe</label>
                <select name="exerciseType" value={form.exerciseType} onChange={handleChange}>
                  {EXERCISE_TYPES.map((t) => (
                    <option key={t.value} value={t.value}>{t.label}</option>
                  ))}
                </select>
              </div>
              <div className="form-group">
                <label>Datum</label>
                <input type="date" name="workoutDate" value={form.workoutDate} onChange={handleChange} />
              </div>
              <div className="form-row">
                <div className="form-group">
                  <label>Trajanje (min)</label>
                  <input type="number" name="durationInMinutes" value={form.durationInMinutes} onChange={handleChange} min="1" placeholder="npr. 45" />
                </div>
                <div className="form-group">
                  <label>Kalorije</label>
                  <input type="number" name="caloriesBurned" value={form.caloriesBurned} onChange={handleChange} min="0" placeholder="npr. 300" />
                </div>
              </div>
              <div className="form-group">
                <label>Težina: <strong>{form.difficulty}</strong></label>
                <input type="range" name="difficulty" min="1" max="10" value={form.difficulty} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label>Zamor: <strong>{form.fatigue}</strong></label>
                <input type="range" name="fatigue" min="1" max="10" value={form.fatigue} onChange={handleChange} />
              </div>
              <div className="form-group">
                <label>Beleška</label>
                <textarea name="notes" value={form.notes} onChange={handleChange} rows={3} placeholder="Opciono..." />
              </div>
              {formError && <p className="form-error">{formError}</p>}
              <button type="submit" className="btn-primary" disabled={formLoading}>
                {formLoading ? 'Dodavanje...' : 'Dodaj trening'}
              </button>
            </form>
          </div>

          {/* RIGHT — WORKOUT LIST */}
          <div className="dashboard-col">
            <h2 className="col-title">Moji treninzi</h2>
            {workouts.length === 0 ? (
              <p className="empty-state">Još nema treninga.</p>
            ) : (
              <ul className="workout-list">
                {workouts.map((w) => (
                  <li key={w.id} className="workout-item">
                    <div className="workout-item-left">
                      <span className="workout-type-badge">{EXERCISE_LABELS[w.exerciseType]}</span>
                      <span className="workout-date">{new Date(w.workoutDate).toLocaleDateString('sr-RS')}</span>
                    </div>
                    <div className="workout-item-right">
                      <span>{w.durationInMinutes} min</span>
                      <span>{w.caloriesBurned} kcal</span>
                      <span>Težina: {w.difficulty}/10</span>
                      <span>Zamor: {w.fatigue}/10</span>
                    </div>
                  </li>
                ))}
              </ul>
            )}
          </div>
        </section>

        {/* MONTHLY PROGRESS */}
        <section className="progress-section">
          <div className="progress-header">
            <h2 className="col-title">Mesečni napredak</h2>
            <div className="progress-selectors">
              <select value={progressMonth} onChange={(e) => setProgressMonth(Number(e.target.value))}>
                {MONTHS.map((m, i) => (
                  <option key={i + 1} value={i + 1}>{m}</option>
                ))}
              </select>
              <select value={progressYear} onChange={(e) => setProgressYear(Number(e.target.value))}>
                {[2024, 2025, 2026, 2027].map((y) => (
                  <option key={y} value={y}>{y}</option>
                ))}
              </select>
            </div>
          </div>

          {progressLoading ? (
            <p className="empty-state">Učitavanje...</p>
          ) : progress.length === 0 ? (
            <p className="empty-state">Nema podataka za ovaj mesec.</p>
          ) : (
            <div className="progress-cards">
              {progress.map((w) => (
                <div key={w.week} className="progress-card">
                  <div className="progress-card-title">Nedelja {w.week}</div>
                  <div className="progress-stat">
                    <span className="progress-stat-label">Treninga</span>
                    <span className="progress-stat-value">{w.totalWorkouts}</span>
                  </div>
                  <div className="progress-stat">
                    <span className="progress-stat-label">Minuta</span>
                    <span className="progress-stat-value">{w.totalDurationInMinutes}</span>
                  </div>
                  <div className="progress-stat">
                    <span className="progress-stat-label">Avg težina</span>
                    <span className="progress-stat-value">{w.averageDifficulty}</span>
                  </div>
                  <div className="progress-stat">
                    <span className="progress-stat-label">Avg zamor</span>
                    <span className="progress-stat-value">{w.averageFatigue}</span>
                  </div>
                </div>
              ))}
            </div>
          )}
        </section>

    </main>
  );
}
