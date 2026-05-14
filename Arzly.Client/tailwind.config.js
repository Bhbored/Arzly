module.exports = {
  content: [
    "./**/*.razor",
    "./**/*.cshtml",
    "./**/*.html",
    "./wwwroot/js/**/*.js",
  ],
  theme: {
    extend: {
      colors: {
        primary: "var(--color-primary)",
        secondary: "var(--color-secondary)",
        accent: "var(--color-accent)",
        background: "var(--color-background)",
        surface: "var(--color-surface)",
        error: "var(--color-error)",
      },
    },
  },
  plugins: [],
};
