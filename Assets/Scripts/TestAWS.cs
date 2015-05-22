using UnityEngine;
using System.Collections;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.SecurityToken;
using System;
using Amazon;

public class TestAWS : MonoBehaviour
{
	public string IDENTITY_POOL_ID;
	public static readonly RegionEndpoint COGNITO_REGION = RegionEndpoint.USEast1;

	private CognitoAWSCredentials _credentials;

	void Start ()
	{
		TimeSpan TS_TIMEOUT = TimeSpan.FromSeconds(1);

		this._credentials = new CognitoAWSCredentials(
			identityPoolId: IDENTITY_POOL_ID, accountId: null,
			unAuthRoleArn: null, authRoleArn: null,
			cibClient: new AmazonCognitoIdentityClient(
				new AnonymousAWSCredentials(),
				new AmazonCognitoIdentityConfig
					{ Timeout = TS_TIMEOUT, RegionEndpoint = COGNITO_REGION }
			),
			stsClient: new AmazonSecurityTokenServiceClient(
				new AnonymousAWSCredentials(),
				new AmazonSecurityTokenServiceConfig
					{ Timeout = TS_TIMEOUT, RegionEndpoint = COGNITO_REGION }
			)
		);

		DateTime before = DateTime.Now;
		Debug.Log("Time before: " + before);
		this._credentials.GetCredentialsAsync(
			_result => {
				Debug.Log( "Time after: " + DateTime.Now + ", duration: " + (DateTime.Now - before) );
				
				Debug.Log(_result);
				if(null != _result.Exception)
				{
					Debug.LogException(_result.Exception);
					return;
				}
				Debug.Log(_result.Response);
				Debug.Log( this._credentials.GetIdentityId() );
			}
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}