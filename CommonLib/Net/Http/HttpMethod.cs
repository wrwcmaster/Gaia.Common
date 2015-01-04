using System;

namespace Gaia.CommonLib.Net.Http
{
	public sealed class HttpMethod {

		private readonly string _value;

		public static readonly HttpMethod GET = new HttpMethod ("GET");
		public static readonly HttpMethod POST = new HttpMethod ("POST");     

		private HttpMethod(string value){
			this._value = value;
		}

		public override string ToString(){
			return _value;
		}

	}
}

