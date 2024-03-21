use std::env;

use axum::{routing::get, Router};
use database::setup_database;
use deadpool_tiberius::Manager;
use dotenv::dotenv;
use handlers::root;
use tokio::net::TcpListener;

pub mod database;
pub mod models;
pub mod handlers;

#[tokio::main]
async fn main() {
    dotenv().ok();

    setup_database().await.unwrap();

    let connection_string = env::var("CONNECTION_STRING").expect("CONNECTION_STRING must be set");

    let pool = Manager::from_ado_string(&connection_string)
        .unwrap()
        .max_size(20)
        .create_pool()
        .unwrap();

    let app = Router::new().route("/", get(root)).with_state(pool);

    let listener = TcpListener::bind("0.0.0.0:3000").await.unwrap();
    axum::serve(listener, app).await.unwrap()
}

