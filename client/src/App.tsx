import { Routes, Route, Link } from "react-router-dom";
import "./App.css";
import Discover from "./pages/Discover";
import EventDetails from "./pages/EventDetails";
import Checkout from "./pages/Checkout";
import MyTickets from "./pages/MyTickets";
import Reviews from "./pages/Reviews";
import AdminDashboard from "./admin/AdminDashboard";
import ManageEvents from "./admin/ManageEvents";
import PromoCodes from "./admin/PromoCodes";
import Refunds from "./admin/Refunds";
import AdminLogin from "./admin/AdminLogin";
import AdminRoute from "./admin/AdminRoute";
import AdminLayout from "./admin/AdminLayout";


export default function App() {
  return (
    <div className="shell">
      <header>
        <Link className="brand" to="/">Eventify</Link>
        <nav className="nav">
          <Link to="/">Discover</Link>
          <Link to="/me/tickets">My Tickets</Link>
          <Link to="/admin">Admin</Link>
        </nav>
      </header>

      <Routes>
        {/* Public / User Routes */}
        <Route path="/" element={<Discover />} />
        <Route path="/events/:id" element={<EventDetails />} />
        <Route path="/events/:id/checkout" element={<Checkout />} />
        <Route path="/events/:id/reviews" element={<Reviews />} />
        <Route path="/me/tickets" element={<MyTickets />} />

        {/* Admin Login */}
        <Route path="/admin/login" element={<AdminLogin />} />

        {/* Protected Admin Area */}
        <Route
          path="/admin"
          element={
            <AdminRoute>
              <AdminLayout />
            </AdminRoute>
          }
        >
          <Route index element={<AdminDashboard />} />
          <Route path="events" element={<ManageEvents />} />
          <Route path="promocodes" element={<PromoCodes />} />
          <Route path="refunds" element={<Refunds />} />
        </Route>
      </Routes>
    </div>
  );
}