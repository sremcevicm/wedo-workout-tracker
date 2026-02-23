import axiosInstance from './axiosInstance';

export const getWorkouts = () =>
  axiosInstance.get('/workouts').then((r) => r.data);

export const createWorkout = (data) =>
  axiosInstance.post('/workouts', data).then((r) => r.data);

export const getMonthlyProgress = (year, month) =>
  axiosInstance.get(`/progress/${year}/${month}`).then((r) => r.data);
