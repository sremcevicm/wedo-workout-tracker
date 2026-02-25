import { useEffect, useState } from 'react';
import Button from '../Button/Button';
import { EXERCISE_TYPES } from '../../constants/exerciseTypes';
import './WorkoutModal.css';

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

function workoutToForm(w) {
  return {
    exerciseType: w.exerciseType,
    durationInMinutes: w.durationInMinutes,
    caloriesBurned: w.caloriesBurned,
    difficulty: w.difficulty,
    fatigue: w.fatigue,
    workoutDate: w.workoutDate.split('T')[0],
    notes: w.notes ?? '',
  };
}

export default function WorkoutModal({ isOpen, workout, onClose, onSave, onDelete }) {
  const isEdit = workout != null;
  const [form, setForm] = useState(EMPTY_FORM);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (isOpen) {
      setForm(isEdit ? workoutToForm(workout) : EMPTY_FORM);
      setError('');
    }
  }, [isOpen, workout]);

  if (!isOpen) return null;

  function handleChange(e) {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');
    if (!form.durationInMinutes || !form.caloriesBurned) {
      setError('Trajanje i kalorije su obavezni.');
      return;
    }
    setLoading(true);
    try {
      await onSave({
        id: workout?.id,
        exerciseType: Number(form.exerciseType),
        durationInMinutes: Number(form.durationInMinutes),
        caloriesBurned: Number(form.caloriesBurned),
        difficulty: Number(form.difficulty),
        fatigue: Number(form.fatigue),
        workoutDate: new Date(form.workoutDate).toISOString(),
        notes: form.notes || null,
      });
      onClose();
    } catch {
      setError('Greška pri čuvanju treninga.');
    } finally {
      setLoading(false);
    }
  }

  async function handleDelete() {
    if (!window.confirm('Da li ste sigurni da želite da obrišete ovaj trening?')) return;
    setLoading(true);
    try {
      await onDelete(workout.id);
      onClose();
    } catch {
      setError('Greška pri brisanju treninga.');
    } finally {
      setLoading(false);
    }
  }

  function handleBackdropClick(e) {
    if (e.target === e.currentTarget) onClose();
  }

  return (
    <div className="modal-backdrop" onClick={handleBackdropClick}>
      <div className="modal-box">
        <div className="modal-header">
          <h2 className="modal-title">{isEdit ? 'Izmeni trening' : 'Dodaj trening'}</h2>
          <button className="modal-close" onClick={onClose} aria-label="Zatvori">✕</button>
        </div>

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
              <input
                type="number" name="durationInMinutes"
                value={form.durationInMinutes} onChange={handleChange}
                min="1" placeholder="npr. 45"
              />
            </div>
            <div className="form-group">
              <label>Kalorije</label>
              <input
                type="number" name="caloriesBurned"
                value={form.caloriesBurned} onChange={handleChange}
                min="0" placeholder="npr. 300"
              />
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

          {error && <p className="form-error">{error}</p>}

          <div className="modal-actions">
            {isEdit && (
              <Button variant="danger" onClick={handleDelete} disabled={loading}>
                Obriši trening
              </Button>
            )}
            <div className="modal-actions-right">
              <Button variant="secondary" onClick={onClose} disabled={loading}>
                Otkaži
              </Button>
              <Button type="submit" disabled={loading}>
                {loading ? 'Čuvanje...' : isEdit ? 'Sačuvaj izmene' : 'Dodaj trening'}
              </Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}
