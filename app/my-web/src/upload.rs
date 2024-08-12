use actix_multipart::Multipart;
use actix_web::{post, web, Error, HttpResponse};
use futures::{StreamExt, TryStreamExt};
use serde::{Deserialize, Serialize};
use std::collections::HashMap;
use std::io::Write;
use std::path::Path;
use log::info;

#[derive(Deserialize, Serialize)]
pub struct FileUploadModel {
    description: String,
    category: String,
    upload_date: String,
    metadata: HashMap<String, String>,
}

#[derive(Serialize)]
struct UploadResponse {
    file_name: String,
    description: String,
    category: String,
    upload_date: String,
    metadata_count: usize,
}

#[post("/upload")]
pub async fn upload_file(mut payload: Multipart) -> Result<HttpResponse, Error> {
    info!("Upload API called");

    let mut model = FileUploadModel {
        description: String::new(),
        category: String::new(),
        upload_date: String::new(),
        metadata: HashMap::new(),
    };
    let mut file_name = String::new();

    while let Ok(Some(mut field)) = payload.try_next().await {
        let content_disposition = field.content_disposition();
        let name = content_disposition.get_name().unwrap_or("");

        match name {
            "file" => {
                file_name = content_disposition
                    .get_filename()
                    .unwrap_or("unknown")
                    .to_string();
                let filepath = Path::new("uploads").join(&file_name);

                info!("Uploading file: {}", file_name);

                // Create uploads directory if it doesn't exist
                std::fs::create_dir_all("uploads")?;

                let mut f = web::block(|| std::fs::File::create(filepath))
                    .await
                    .unwrap()?;

                while let Some(chunk) = field.next().await {
                    let data = chunk.unwrap();
                    f = web::block(move || f.write_all(&data).map(|_| f)).await??;
                }

                info!("File uploaded successfully: {}", file_name);
            }
            "description" => {
                let data = field.next().await.unwrap()?;
                model.description = String::from_utf8(data.to_vec()).unwrap();
                info!("Description received: {}", model.description);
            }
            "category" => {
                let data = field.next().await.unwrap()?;
                model.category = String::from_utf8(data.to_vec()).unwrap();
                info!("Category received: {}", model.category);
            }
            "upload_date" => {
                let data = field.next().await.unwrap()?;
                model.upload_date = String::from_utf8(data.to_vec()).unwrap();
                info!("Upload date received: {}", model.upload_date);
            }
            "metadata" => {
                let data = field.next().await.unwrap()?;
                let metadata_str = String::from_utf8(data.to_vec()).unwrap();
                model.metadata = serde_json::from_str(&metadata_str).unwrap();
                info!("Metadata received: {:?}", model.metadata);
            }
            _ => {}
        }
    }

    let response = UploadResponse {
        file_name,
        description: model.description,
        category: model.category,
        upload_date: model.upload_date,
        metadata_count: model.metadata.len(),
    };

    info!("Upload completed successfully");

    Ok(HttpResponse::Ok().json(response))
}
