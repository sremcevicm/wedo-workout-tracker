function fmtDate(str) {
  const d = new Date(str);
  return d.toLocaleDateString('sr-Latn-RS', { day: 'numeric', month: 'short' });
}

function fmtMinutes(total) {
  const h = Math.floor(total / 60);
  const m = total % 60;
  if (h === 0) return `${m}min`;
  if (m === 0) return `${h}h`;
  return `${h}h ${m}min`;
}

export default function ProgressCard({ week, weekStart, weekEnd, totalWorkouts, totalDurationInMinutes, averageDifficulty, averageFatigue }) {
  return (
    <div className="progress-card">
      <div className="progress-card-title">Nedelja {week}</div>
      <div className="progress-card-range">{fmtDate(weekStart)} — {fmtDate(weekEnd)}</div>
      <div className="progress-stat">
        <span className="progress-stat-label">Treninga</span>
        <span className="progress-stat-value">{totalWorkouts}</span>
      </div>
      <div className="progress-stat">
        <span className="progress-stat-label">Vreme</span>
        <span className="progress-stat-value">{fmtMinutes(totalDurationInMinutes)}</span>
      </div>
      <div className="progress-stat">
        <span className="progress-stat-label">Prosečna težina</span>
        <span className="progress-stat-value">{averageDifficulty}</span>
      </div>
      <div className="progress-stat">
        <span className="progress-stat-label">Prosečan zamor</span>
        <span className="progress-stat-value">{averageFatigue}</span>
      </div>
    </div>
  );
}
