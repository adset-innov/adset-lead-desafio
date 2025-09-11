/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src//*.{html,ts}"
  ],
  theme: {
    extend: {
      colors: {
        brand: {
          50: "#fff7ed",
          100: "#ffedd5",
          200: "#fed7aa",
          300: "#fdba74",
          400: "#fb923c",   // laranja (botões/realces)
          500: "#f97316",
          600: "#ea580c",
          700: "#c2410c",
          800: "#9a3412",
          900: "#7c2d12",
        },
        slateish: "#f5f7fb"
      },
      boxShadow: {
        card: "0 2px 10px rgba(0,0,0,0.06)",
      },
      borderRadius: {
        xl2: "1rem"
      }
    },
  },
  plugins: [],
}
