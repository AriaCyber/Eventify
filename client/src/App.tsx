import { Routes, Route, Link } from "react-router-dom";
import "./App.css";
import Discover from "./pages/Discover";
import EventDetails from "./pages/EventDetails";
import Checkout from "./pages/Checkout";
import MyTickets from "./pages/MyTickets";
import Reviews from "./pages/Reviews";

export default function App(){
  return (
    <div className="shell">
      <header>
        <Link className="brand" to="/">Eventify</Link>
        <nav className="nav">
          <Link to="/">Discover</Link>
          <Link to="/me/tickets">My Tickets</Link>
        </nav>
      </header>
      <Routes>
        <Route path="/" element={<Discover/>} />
        <Route path="/events/:id" element={<EventDetails/>} />
        <Route path="/events/:id/checkout" element={<Checkout/>} />
        <Route path="/events/:id/reviews" element={<Reviews/>} />
        <Route path="/me/tickets" element={<MyTickets/>} />
      </Routes>
    </div>
  );
}
