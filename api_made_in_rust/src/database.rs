use std::env;

use anyhow::{Ok, Result};
use dotenv::dotenv;
use tiberius::{Client, Config};
use tokio::{fs, net::TcpStream};
use tokio_util::compat::{Compat, TokioAsyncWriteCompatExt};



pub async fn setup_database() -> Result<()> {
    create_database().await.unwrap();

    create_tables().await.unwrap();

    populate_database().await.unwrap();

    Ok(())
}

async fn get_connection() -> Result<Client<Compat<TcpStream>>> {
    dotenv().ok();

    let connection_string =
        env::var("CONNECTION_STRING_INITIAL").expect("CONNECTION_STRING_INITIAL must be set");

    let config = Config::from_ado_string(&connection_string)?;
    let tcp = TcpStream::connect(config.get_addr()).await?;
    tcp.set_nodelay(true)?;

    let client = Client::connect(config, tcp.compat_write()).await?;
    Ok(client)
}

async fn create_database() -> Result<()> {
    let mut connection = get_connection().await.unwrap();
    let query = r#"CREATE DATABASE [FakeStore]"#;
    connection.simple_query(query).await?;
    Ok(())
}

async fn create_tables() -> Result<()> {
    let mut connection = get_connection().await.unwrap();
    let query = fs::read_to_string("src/mssql/script.sql").await.unwrap();
    connection.simple_query(query).await?;
    Ok(())
}

async fn populate_database() -> Result<()> {
    let mut connection = get_connection().await.unwrap();
    let query = fs::read_to_string("src/mssql/insert.sql").await.unwrap();
    connection.simple_query(query).await?;
    Ok(())
}
