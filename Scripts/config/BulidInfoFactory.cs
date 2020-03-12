    
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class Model {
	public int id; // 造型
	public int levelRequire; // 要求等级
}

public class BulidInfo {
	public int id; // 编号
	public string name; // 名称
	public double productCoefficient; // 产出系数
	public double timeCoefficient; // 时间系数
	public double time; // 时间间隔
	public int unlock; // 解锁值
	public int limitLevel; // 上限等级
	[XmlArrayAttribute("modelArray")]
	public Model[] model; 
	public string description; // 描述
}

[XmlRoot("BulidInfo")]
public class BulidInfoFactory {
    [XmlElement("data")]
    public BulidInfo[] BulidInfoArray {get; set;}
    private static BulidInfoFactory _instance;

    BulidInfoFactory() {}

    const string configXML = @"
    <BulidInfo>
    <data><id>1</id><name>主基地</name><productCoefficient>5</productCoefficient><timeCoefficient>1</timeCoefficient><time>10</time><unlock>1</unlock><limitLevel>50</limitLevel><modelArray><Model><id>101</id><levelRequire>1</levelRequire></Model><Model><id>102</id><levelRequire>10</levelRequire></Model><Model><id>103</id><levelRequire>25</levelRequire></Model><Model><id>104</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>2</id><name>兵工厂</name><productCoefficient>1.2</productCoefficient><timeCoefficient>1</timeCoefficient><time>5</time><unlock>2</unlock><limitLevel>50</limitLevel><modelArray><Model><id>201</id><levelRequire>1</levelRequire></Model><Model><id>202</id><levelRequire>10</levelRequire></Model><Model><id>203</id><levelRequire>25</levelRequire></Model><Model><id>204</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>3</id><name>实验室</name><productCoefficient>1.92</productCoefficient><timeCoefficient>1</timeCoefficient><time>8</time><unlock>3</unlock><limitLevel>50</limitLevel><modelArray><Model><id>301</id><levelRequire>1</levelRequire></Model><Model><id>302</id><levelRequire>10</levelRequire></Model><Model><id>303</id><levelRequire>25</levelRequire></Model><Model><id>304</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>4</id><name>矿场</name><productCoefficient>3.5999999999999996</productCoefficient><timeCoefficient>1</timeCoefficient><time>15</time><unlock>4</unlock><limitLevel>50</limitLevel><modelArray><Model><id>401</id><levelRequire>1</levelRequire></Model><Model><id>402</id><levelRequire>10</levelRequire></Model><Model><id>403</id><levelRequire>25</levelRequire></Model><Model><id>404</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>5</id><name>运输厂</name><productCoefficient>7.199999999999999</productCoefficient><timeCoefficient>1</timeCoefficient><time>30</time><unlock>5</unlock><limitLevel>50</limitLevel><modelArray><Model><id>501</id><levelRequire>1</levelRequire></Model><Model><id>502</id><levelRequire>10</levelRequire></Model><Model><id>503</id><levelRequire>25</levelRequire></Model><Model><id>504</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>6</id><name>仓库1</name><productCoefficient>0.54</productCoefficient><timeCoefficient>1</timeCoefficient><time>3</time><unlock>6</unlock><limitLevel>50</limitLevel><modelArray><Model><id>601</id><levelRequire>1</levelRequire></Model><Model><id>602</id><levelRequire>10</levelRequire></Model><Model><id>603</id><levelRequire>25</levelRequire></Model><Model><id>604</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>7</id><name>仓库2</name><productCoefficient>0.54</productCoefficient><timeCoefficient>1</timeCoefficient><time>3</time><unlock>8</unlock><limitLevel>50</limitLevel><modelArray><Model><id>601</id><levelRequire>1</levelRequire></Model><Model><id>602</id><levelRequire>10</levelRequire></Model><Model><id>603</id><levelRequire>25</levelRequire></Model><Model><id>604</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>8</id><name>仓库3</name><productCoefficient>0.54</productCoefficient><timeCoefficient>1</timeCoefficient><time>3</time><unlock>10</unlock><limitLevel>50</limitLevel><modelArray><Model><id>601</id><levelRequire>1</levelRequire></Model><Model><id>602</id><levelRequire>10</levelRequire></Model><Model><id>603</id><levelRequire>25</levelRequire></Model><Model><id>604</id><levelRequire>40</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>9</id><name>回收场</name><productCoefficient>0</productCoefficient><timeCoefficient>1</timeCoefficient><time>0</time><unlock>25</unlock><limitLevel>1</limitLevel><modelArray><Model><id>701</id><levelRequire>1</levelRequire></Model><Model><id>701</id><levelRequire>1</levelRequire></Model><Model><id>701</id><levelRequire>1</levelRequire></Model><Model><id>701</id><levelRequire>1</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>10</id><name>商店</name><productCoefficient>0</productCoefficient><timeCoefficient>1</timeCoefficient><time>0</time><unlock>1</unlock><limitLevel>1</limitLevel><modelArray><Model><id>702</id><levelRequire>1</levelRequire></Model><Model><id>702</id><levelRequire>1</levelRequire></Model><Model><id>702</id><levelRequire>1</levelRequire></Model><Model><id>702</id><levelRequire>1</levelRequire></Model></modelArray><description>这是一句描述</description></data><data><id>11</id><name>福利店</name><productCoefficient>0</productCoefficient><timeCoefficient>1</timeCoefficient><time>0</time><unlock>1</unlock><limitLevel>1</limitLevel><modelArray><Model><id>703</id><levelRequire>1</levelRequire></Model><Model><id>703</id><levelRequire>1</levelRequire></Model><Model><id>703</id><levelRequire>1</levelRequire></Model><Model><id>703</id><levelRequire>1</levelRequire></Model></modelArray><description>这是一句描述</description></data>
</BulidInfo>
    ";

    public static BulidInfoFactory Instance {
        get {
            if (_instance == null) {
                XmlSerializer xs = new XmlSerializer(typeof(BulidInfoFactory));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(configXML));
                _instance = xs.Deserialize(ms) as BulidInfoFactory;
            }
            return _instance;
        }
    }
}
    