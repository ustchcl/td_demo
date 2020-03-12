    
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class ProduceModel {
	public int id; // 编号
	public double num; // 产出量
}

[XmlRoot("ProduceModel")]
public class ProduceModelFactory {
    [XmlElement("data")]
    public ProduceModel[] ProduceModelArray {get; set;}
    private static ProduceModelFactory _instance;

    ProduceModelFactory() {}

    const string configXML = @"
    <ProduceModel>
    <data><id>0</id><num>0</num></data><data><id>1</id><num>5</num></data><data><id>2</id><num>10</num></data><data><id>3</id><num>20</num></data><data><id>4</id><num>40</num></data><data><id>5</id><num>80</num></data><data><id>6</id><num>160</num></data><data><id>7</id><num>320</num></data><data><id>8</id><num>640</num></data><data><id>9</id><num>1280</num></data><data><id>10</id><num>2560</num></data><data><id>11</id><num>5120</num></data><data><id>12</id><num>10200</num></data><data><id>13</id><num>20400</num></data><data><id>14</id><num>40900</num></data><data><id>15</id><num>81900</num></data><data><id>16</id><num>163000</num></data><data><id>17</id><num>327000</num></data><data><id>18</id><num>655000</num></data><data><id>19</id><num>1310000</num></data><data><id>20</id><num>2620000</num></data><data><id>21</id><num>5240000</num></data><data><id>22</id><num>10400000</num></data><data><id>23</id><num>20900000</num></data><data><id>24</id><num>41900000</num></data><data><id>25</id><num>83800000</num></data><data><id>26</id><num>167000000</num></data><data><id>27</id><num>335000000</num></data><data><id>28</id><num>671000000</num></data><data><id>29</id><num>1340000000</num></data><data><id>30</id><num>2680000000</num></data><data><id>31</id><num>5360000000</num></data><data><id>32</id><num>10700000000</num></data><data><id>33</id><num>21400000000</num></data><data><id>34</id><num>42900000000</num></data><data><id>35</id><num>85800000000</num></data><data><id>36</id><num>171000000000</num></data><data><id>37</id><num>343000000000</num></data><data><id>38</id><num>687000000000</num></data><data><id>39</id><num>1370000000000</num></data><data><id>40</id><num>2740000000000</num></data><data><id>41</id><num>5490000000000</num></data><data><id>42</id><num>10900000000000</num></data><data><id>43</id><num>21900000000000</num></data><data><id>44</id><num>43900000000000</num></data><data><id>45</id><num>87900000000000</num></data><data><id>46</id><num>175000000000000</num></data><data><id>47</id><num>351000000000000</num></data><data><id>48</id><num>703000000000000</num></data><data><id>49</id><num>1400000000000000</num></data><data><id>50</id><num>2810000000000000</num></data>
</ProduceModel>
    ";

    public static ProduceModelFactory Instance {
        get {
            if (_instance == null) {
                XmlSerializer xs = new XmlSerializer(typeof(ProduceModelFactory));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(configXML));
                _instance = xs.Deserialize(ms) as ProduceModelFactory;
            }
            return _instance;
        }
    }
}
    