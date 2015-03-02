using System;
using System.Collections;
using Newtonsoft.Json;

class Utilities {

  public static T StringToEnum<T>( string value ) {
      return (T) Enum.Parse( typeof( T ), value, true );
  }

  public static T Copy<T>(T source) {
    if (Object.ReferenceEquals(source, null)) {
      return default(T);
    }

    return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
  }

}
