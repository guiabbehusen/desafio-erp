import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import 'bootstrap/dist/css/bootstrap.min.css';
import { App } from './App'
import { createBrowserRouter, Route, Router, RouterProvider } from "react-router-dom";
import { Cadastro } from './pages/Cadastro';
import { EntrarEmContato } from './pages/Contato';
import { Home } from './pages/Home';
import { AlterarDados } from './pages/AlterarDados';

const router = createBrowserRouter([

  {
    path: "/",
    element: <App />,

  },
  {
    path: "/cadastro",
    element: <Cadastro />
  },
  {
    path: "/entrar_em_contato",
    element: <EntrarEmContato />
  },
  {
    path: "/home",
    element: <Home />
  },
  {
    path: "/alterar_dados",
    element: <AlterarDados />
  }

]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
