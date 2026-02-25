import './Button.css';

export default function Button({ variant = 'primary', type = 'button', disabled, onClick, title, children, ...props }) {
  return (
    <button
      type={type}
      className={`btn btn-${variant}`}
      disabled={disabled}
      onClick={onClick}
      title={title}
      {...props}
    >
      {children}
    </button>
  );
}
