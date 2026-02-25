export const EXERCISE_TYPES = [
  { value: 0, label: 'Cardio' },
  { value: 1, label: 'Strength' },
  { value: 2, label: 'Flexibility' },
];

export const EXERCISE_LABELS = Object.fromEntries(
  EXERCISE_TYPES.map(({ value, label }) => [value, label])
);
