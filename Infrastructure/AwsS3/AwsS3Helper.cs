using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Core.Interfaces;

namespace Infrastructure.Helpers.AwsS3; 

public class AwsS3Helper: IObjectStorageProvider {
    private static readonly IAmazonS3 client = new AmazonS3Client();
    private static readonly string BUCKET_NAME = Environment.GetEnvironmentVariable("S3_BUCKET_NAME");
    private static readonly string BUCKET_URL = Environment.GetEnvironmentVariable("S3_BUCKET_URL");
    private static readonly string S3_KEY = Environment.GetEnvironmentVariable("S3_KEY"); 
    
    public async Task<string> UploadImageAsync(string imagePath) {
        var uploadRequest = new PutObjectRequest {
            BucketName = BUCKET_NAME,
            Key = S3_KEY,
            FilePath = imagePath,
        };
        
        var response = await client.PutObjectAsync(uploadRequest);
        if (response.HttpStatusCode == HttpStatusCode.OK) { 
            // TODO SHOULD RETURN THE S3 OBJECT ID
            return response.VersionId;
        }
        throw new AwsS3Exception("Exception While Uploading to s3"); 
    }

    public async Task<GetImageResponse> GetImageAsync(string bucketId) {
        var GetObjectRequest = new GetObjectRequest {
            Key = S3_KEY,
            BucketName = BUCKET_NAME,
        };
        var response = await client.GetObjectAsync(GetObjectRequest);
        if (response.HttpStatusCode == HttpStatusCode.OK) {
            return new GetImageResponse(); 
        }

        throw new AwsS3Exception("Exception While Retrieving The Image"); 
    }

    public 
        async Task<bool> DeleteImageFromBucket(string imageId) {
        throw new NotImplementedException(); 
    }
}