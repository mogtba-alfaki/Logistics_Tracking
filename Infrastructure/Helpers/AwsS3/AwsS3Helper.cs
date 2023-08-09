using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Infrastructure.Util;

namespace Infrastructure.Helpers.AwsS3; 

public static class AwsS3Helper {
    private static readonly IAmazonS3 client = new AmazonS3Client();
    private static readonly string BUCKET_NAME = Environment.GetEnvironmentVariable("S3_BUCKET_NAME");
    private static readonly string BUCKET_URL = Environment.GetEnvironmentVariable("S3_BUCKET_URL");
    private static readonly string S3_KEY = Environment.GetEnvironmentVariable("S3_KEY"); 
    
    public static async Task<string> UploadImageAsync(string imagePath) {
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

    public static async Task<GetObjectResponse> GetImageAsync(string bucketId) {
        throw new NotImplementedException(); 
    }

    public static async Task<bool> DeleteImageFromBucket(string imageId) {
        throw new NotImplementedException(); 
    }
}