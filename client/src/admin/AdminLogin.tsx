import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function AdminLogin(){
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const nav = useNavigate();

  function login(){
    if (
      email === "admin@eventify.com" &&
      password === "admin123"
    ) {
      localStorage.setItem("isAdmin", "true");
      nav("/admin");
    } else {
      alert("Invalid admin credentials");
    }
  }

  return (
    <div>
      <h2>Admin Login</h2>

      <input
        placeholder="Email"
        value={email}
        onChange={e=>setEmail(e.target.value)}
      /><br/><br/>

      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={e=>setPassword(e.target.value)}
      /><br/><br/>

      <button onClick={login}>Login</button>
    </div>
  );
}
