use serde::Deserialize;
use time::OffsetDateTime;

#[derive(Deserialize)]
pub struct Comment {
    #[serde(rename = "Id")]
    pub id: u8,
    #[serde(rename = "StockId")]
    pub stock_id: u8,
    #[serde(rename = "Title")]
    pub title: String,
    #[serde(rename = "Content")]
    pub content: String,
    #[serde(rename = "CreatedOn")]
    pub created_on: OffsetDateTime,
    #[serde(rename = "AppUserId")]
    pub app_user_id: u8,
}
