import { useEffect, useState } from 'react';
import { getWorkouts, createWorkout, updateWorkout, deleteWorkout, getMonthlyProgress } from '../../api/workoutsApi';
import WorkoutModal from '../../components/WorkoutModal/WorkoutModal';
import ProgressCard from '../../components/ProgressCard/ProgressCard';
import Button from '../../components/Button/Button';
import { EXERCISE_LABELS } from '../../constants/exerciseTypes';
import { MONTHS } from '../../constants/months';
import './dashboard.css';

export default function DashboardPage() {

  const [workouts, setWorkouts] = useState([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [selectedWorkout, setSelectedWorkout] = useState(null);

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

  function openAddModal() {
    setSelectedWorkout(null);
    setModalOpen(true);
  }

  function openEditModal(workout) {
    setSelectedWorkout(workout);
    setModalOpen(true);
  }

  function closeModal() {
    setModalOpen(false);
    setSelectedWorkout(null);
  }

  async function handleSave(data) {
    if (data.id) {
      await updateWorkout(data.id, data);
    } else {
      await createWorkout(data);
    }
    const [updated, updatedProgress] = await Promise.all([
      getWorkouts(),
      getMonthlyProgress(progressYear, progressMonth),
    ]);
    setWorkouts(updated);
    setProgress(updatedProgress);
  }

  async function handleDelete(id) {
    await deleteWorkout(id);
    const [updated, updatedProgress] = await Promise.all([
      getWorkouts(),
      getMonthlyProgress(progressYear, progressMonth),
    ]);
    setWorkouts(updated);
    setProgress(updatedProgress);
  }

  const totalWorkouts = workouts.length;
  const totalMinutes = workouts.reduce((s, w) => s + w.durationInMinutes, 0);
  const avgDifficulty = totalWorkouts
    ? (workouts.reduce((s, w) => s + w.difficulty, 0) / totalWorkouts).toFixed(1)
    : '—';
  const avgFatigue = totalWorkouts
    ? (workouts.reduce((s, w) => s + w.fatigue, 0) / totalWorkouts).toFixed(1)
    : '—';

  function fmtMinutes(total) {
    const h = Math.floor(total / 60);
    const m = total % 60;
    if (h === 0) return `${m}min`;
    if (m === 0) return `${h}h`;
    return `${h}h ${m}min`;
  }

  function prevMonth() {
    if (progressMonth === 1) { setProgressMonth(12); setProgressYear((y) => y - 1); }
    else setProgressMonth((m) => m - 1);
  }
  function nextMonth() {
    if (progressMonth === 12) { setProgressMonth(1); setProgressYear((y) => y + 1); }
    else setProgressMonth((m) => m + 1);
  }
  function prevYear() { setProgressYear((y) => y - 1); }
  function nextYear() { setProgressYear((y) => y + 1); }

  return (
    <main className="dashboard-main">

      <section className="summary-cards">
        <div className="summary-card">
          <span className="summary-label">Ukupno treninga</span>
          <span className="summary-value">{totalWorkouts}</span>
        </div>
        <div className="summary-card">
          <span className="summary-label">Ukupno vreme</span>
          <span className="summary-value">{fmtMinutes(totalMinutes)}</span>
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

      <section className="progress-section">
        <div className="progress-header">
          <h2 className="col-title">Mesečni napredak</h2>
          <div className="progress-selectors">
            <div className="picker">
              <button className="picker-arrow" onClick={prevMonth}>&#8249;</button>
              <span className="picker-label">{MONTHS[progressMonth - 1]}</span>
              <button className="picker-arrow" onClick={nextMonth}>&#8250;</button>
            </div>
            <div className="picker">
              <button className="picker-arrow" onClick={prevYear}>&#8249;</button>
              <span className="picker-label">{progressYear}</span>
              <button className="picker-arrow" onClick={nextYear}>&#8250;</button>
            </div>
          </div>
        </div>

        {progressLoading ? (
          <p className="empty-state">Učitavanje...</p>
        ) : progress.length === 0 ? (
          <p className="empty-state">Nema podataka za ovaj mesec.</p>
        ) : (
          <div className="progress-cards">
            {progress.map((w) => (
              <ProgressCard
                key={w.week}
                week={w.week}
                weekStart={w.weekStart}
                weekEnd={w.weekEnd}
                totalWorkouts={w.totalWorkouts}
                totalDurationInMinutes={w.totalDurationInMinutes}
                averageDifficulty={w.averageDifficulty}
                averageFatigue={w.averageFatigue}
              />
            ))}
          </div>
        )}
      </section>

      <section className="workouts-section">
        <div className="section-header">
          <h2 className="col-title">Moji treninzi</h2>
          <Button onClick={openAddModal}>+ Dodaj novi trening</Button>
        </div>
        {workouts.length === 0 ? (
          <p className="empty-state">Još nema treninga. Dodaj prvi!</p>
        ) : (
          <ul className="workout-list">
            {workouts.map((w) => (
              <li key={w.id} className="workout-item" onClick={() => openEditModal(w)}>
                <div className="workout-item-left">
                  <span className="workout-type-badge">{EXERCISE_LABELS[w.exerciseType]}</span>
                  <span className="workout-date">{new Date(w.workoutDate).toLocaleDateString('sr-RS')}</span>
                </div>
                <div className="workout-item-right">
                  <span>{fmtMinutes(w.durationInMinutes)}</span>
                  <span>{w.caloriesBurned} kcal</span>
                  <span>Težina: {w.difficulty}/10</span>
                  <span>Zamor: {w.fatigue}/10</span>
                  <Button
                    variant="delete"
                    onClick={(e) => { e.stopPropagation(); if (window.confirm('Obrisati ovaj trening?')) handleDelete(w.id); }}
                    title="Obriši trening"
                  >
                    X
                  </Button>
                </div>
              </li>
            ))}
          </ul>
        )}
      </section>

      <WorkoutModal
        isOpen={modalOpen}
        workout={selectedWorkout}
        onClose={closeModal}
        onSave={handleSave}
        onDelete={handleDelete}
      />

    </main>
  );
}
