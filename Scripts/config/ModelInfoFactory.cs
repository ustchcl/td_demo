    
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class ModelInfo {
	public int id; // 编号
	public string name; // 模型名称
	public string description; // 描述
}

[XmlRoot("ModelInfo")]
public class ModelInfoFactory {
    [XmlElement("data")]
    public ModelInfo[] ModelInfoArray {get; set;}
    private static ModelInfoFactory _instance;

    ModelInfoFactory() {}

    const string configXML = @"
    <ModelInfo>
    <data><id>1</id><name>箭塔1</name><description>这是一句描述</description></data><data><id>2</id><name>箭塔2</name><description>这是一句描述</description></data><data><id>3</id><name>箭塔3</name><description>这是一句描述</description></data><data><id>4</id><name>箭塔4</name><description>这是一句描述</description></data><data><id>5</id><name>箭塔5</name><description>这是一句描述</description></data><data><id>6</id><name>箭塔6</name><description>这是一句描述</description></data><data><id>7</id><name>箭塔7</name><description>这是一句描述</description></data><data><id>8</id><name>箭塔8</name><description>这是一句描述</description></data><data><id>9</id><name>箭塔9</name><description>这是一句描述</description></data><data><id>10</id><name>箭塔10</name><description>这是一句描述</description></data><data><id>11</id><name>箭塔11</name><description>这是一句描述</description></data><data><id>12</id><name>箭塔12</name><description>这是一句描述</description></data><data><id>13</id><name>箭塔13</name><description>这是一句描述</description></data><data><id>14</id><name>箭塔14</name><description>这是一句描述</description></data><data><id>15</id><name>箭塔15</name><description>这是一句描述</description></data><data><id>16</id><name>箭塔16</name><description>这是一句描述</description></data><data><id>17</id><name>箭塔17</name><description>这是一句描述</description></data><data><id>18</id><name>箭塔18</name><description>这是一句描述</description></data><data><id>19</id><name>箭塔19</name><description>这是一句描述</description></data><data><id>20</id><name>箭塔20</name><description>这是一句描述</description></data><data><id>21</id><name>箭塔21</name><description>这是一句描述</description></data><data><id>22</id><name>箭塔22</name><description>这是一句描述</description></data><data><id>23</id><name>箭塔23</name><description>这是一句描述</description></data><data><id>24</id><name>箭塔24</name><description>这是一句描述</description></data><data><id>25</id><name>箭塔25</name><description>这是一句描述</description></data><data><id>26</id><name>箭塔26</name><description>这是一句描述</description></data><data><id>27</id><name>箭塔27</name><description>这是一句描述</description></data><data><id>28</id><name>箭塔28</name><description>这是一句描述</description></data><data><id>29</id><name>箭塔29</name><description>这是一句描述</description></data><data><id>30</id><name>箭塔30</name><description>这是一句描述</description></data><data><id>31</id><name>箭塔31</name><description>这是一句描述</description></data><data><id>32</id><name>箭塔32</name><description>这是一句描述</description></data><data><id>33</id><name>箭塔33</name><description>这是一句描述</description></data><data><id>34</id><name>箭塔34</name><description>这是一句描述</description></data><data><id>35</id><name>箭塔35</name><description>这是一句描述</description></data><data><id>36</id><name>箭塔36</name><description>这是一句描述</description></data><data><id>37</id><name>箭塔37</name><description>这是一句描述</description></data><data><id>38</id><name>箭塔38</name><description>这是一句描述</description></data><data><id>39</id><name>箭塔39</name><description>这是一句描述</description></data><data><id>40</id><name>箭塔40</name><description>这是一句描述</description></data><data><id>41</id><name>箭塔41</name><description>这是一句描述</description></data><data><id>42</id><name>箭塔42</name><description>这是一句描述</description></data><data><id>43</id><name>箭塔43</name><description>这是一句描述</description></data><data><id>44</id><name>箭塔44</name><description>这是一句描述</description></data><data><id>45</id><name>箭塔45</name><description>这是一句描述</description></data><data><id>46</id><name>箭塔46</name><description>这是一句描述</description></data><data><id>47</id><name>箭塔47</name><description>这是一句描述</description></data><data><id>48</id><name>箭塔48</name><description>这是一句描述</description></data><data><id>49</id><name>箭塔49</name><description>这是一句描述</description></data><data><id>50</id><name>箭塔50</name><description>这是一句描述</description></data><data><id>101</id><name>主基地</name><description>这是一句描述</description></data><data><id>102</id><name>主基地</name><description>这是一句描述</description></data><data><id>103</id><name>主基地</name><description>这是一句描述</description></data><data><id>104</id><name>主基地</name><description>这是一句描述</description></data><data><id>201</id><name>兵工厂</name><description>这是一句描述</description></data><data><id>202</id><name>兵工厂</name><description>这是一句描述</description></data><data><id>203</id><name>兵工厂</name><description>这是一句描述</description></data><data><id>204</id><name>兵工厂</name><description>这是一句描述</description></data><data><id>301</id><name>实验室</name><description>这是一句描述</description></data><data><id>302</id><name>实验室</name><description>这是一句描述</description></data><data><id>303</id><name>实验室</name><description>这是一句描述</description></data><data><id>304</id><name>实验室</name><description>这是一句描述</description></data><data><id>401</id><name>矿场</name><description>这是一句描述</description></data><data><id>402</id><name>矿场</name><description>这是一句描述</description></data><data><id>403</id><name>矿场</name><description>这是一句描述</description></data><data><id>404</id><name>矿场</name><description>这是一句描述</description></data><data><id>501</id><name>运输厂</name><description>这是一句描述</description></data><data><id>502</id><name>运输厂</name><description>这是一句描述</description></data><data><id>503</id><name>运输厂</name><description>这是一句描述</description></data><data><id>504</id><name>运输厂</name><description>这是一句描述</description></data><data><id>601</id><name>仓库1</name><description>这是一句描述</description></data><data><id>602</id><name>仓库1</name><description>这是一句描述</description></data><data><id>603</id><name>仓库1</name><description>这是一句描述</description></data><data><id>604</id><name>仓库1</name><description>这是一句描述</description></data><data><id>701</id><name>回收场</name><description>这是一句描述</description></data><data><id>702</id><name>商店</name><description>这是一句描述</description></data><data><id>703</id><name>福利店</name><description>这是一句描述</description></data>
</ModelInfo>
    ";

    public static ModelInfoFactory Instance {
        get {
            if (_instance == null) {
                XmlSerializer xs = new XmlSerializer(typeof(ModelInfoFactory));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(configXML));
                _instance = xs.Deserialize(ms) as ModelInfoFactory;
            }
            return _instance;
        }
    }
}
    