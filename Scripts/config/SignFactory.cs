    
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class Sign {
	public int id; // 编号
	public int award; // 奖励
}

[XmlRoot("Sign")]
public class SignFactory {
    [XmlElement("data")]
    public Sign[] SignArray {get; set;}
    private static SignFactory _instance;

    SignFactory() {}

    const string configXML = @"
    <Sign>
    <data><id>1</id><award>255</award></data><data><id>2</id><award>385</award></data><data><id>3</id><award>510</award></data><data><id>4</id><award>640</award></data><data><id>5</id><award>770</award></data><data><id>6</id><award>770</award></data><data><id>7</id><award>1020</award></data>
</Sign>
    ";

    public static SignFactory Instance {
        get {
            if (_instance == null) {
                XmlSerializer xs = new XmlSerializer(typeof(SignFactory));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(configXML));
                _instance = xs.Deserialize(ms) as SignFactory;
            }
            return _instance;
        }
    }
}
    