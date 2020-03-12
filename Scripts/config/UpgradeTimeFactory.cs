    
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class UpgradeTime {
	public int id; // 编号
	public int upgradeTime; // 升级时间
	public int finishDiamonds; // 钻石立刻完成
}

[XmlRoot("UpgradeTime")]
public class UpgradeTimeFactory {
    [XmlElement("data")]
    public UpgradeTime[] UpgradeTimeArray {get; set;}
    private static UpgradeTimeFactory _instance;

    UpgradeTimeFactory() {}

    const string configXML = @"
    <UpgradeTime>
    <data><id>1</id><upgradeTime>2</upgradeTime><finishDiamonds>2</finishDiamonds></data><data><id>2</id><upgradeTime>3</upgradeTime><finishDiamonds>3</finishDiamonds></data><data><id>3</id><upgradeTime>5</upgradeTime><finishDiamonds>5</finishDiamonds></data><data><id>4</id><upgradeTime>10</upgradeTime><finishDiamonds>10</finishDiamonds></data><data><id>5</id><upgradeTime>20</upgradeTime><finishDiamonds>20</finishDiamonds></data><data><id>6</id><upgradeTime>30</upgradeTime><finishDiamonds>30</finishDiamonds></data><data><id>7</id><upgradeTime>50</upgradeTime><finishDiamonds>40</finishDiamonds></data><data><id>8</id><upgradeTime>70</upgradeTime><finishDiamonds>50</finishDiamonds></data><data><id>9</id><upgradeTime>90</upgradeTime><finishDiamonds>60</finishDiamonds></data><data><id>10</id><upgradeTime>120</upgradeTime><finishDiamonds>70</finishDiamonds></data><data><id>11</id><upgradeTime>150</upgradeTime><finishDiamonds>80</finishDiamonds></data><data><id>12</id><upgradeTime>200</upgradeTime><finishDiamonds>90</finishDiamonds></data><data><id>13</id><upgradeTime>250</upgradeTime><finishDiamonds>100</finishDiamonds></data><data><id>14</id><upgradeTime>300</upgradeTime><finishDiamonds>120</finishDiamonds></data><data><id>15</id><upgradeTime>450</upgradeTime><finishDiamonds>140</finishDiamonds></data><data><id>16</id><upgradeTime>600</upgradeTime><finishDiamonds>160</finishDiamonds></data><data><id>17</id><upgradeTime>800</upgradeTime><finishDiamonds>180</finishDiamonds></data><data><id>18</id><upgradeTime>1000</upgradeTime><finishDiamonds>200</finishDiamonds></data><data><id>19</id><upgradeTime>1300</upgradeTime><finishDiamonds>220</finishDiamonds></data><data><id>20</id><upgradeTime>1600</upgradeTime><finishDiamonds>240</finishDiamonds></data><data><id>21</id><upgradeTime>2000</upgradeTime><finishDiamonds>260</finishDiamonds></data><data><id>22</id><upgradeTime>2400</upgradeTime><finishDiamonds>280</finishDiamonds></data><data><id>23</id><upgradeTime>2800</upgradeTime><finishDiamonds>300</finishDiamonds></data><data><id>24</id><upgradeTime>3200</upgradeTime><finishDiamonds>320</finishDiamonds></data><data><id>25</id><upgradeTime>3600</upgradeTime><finishDiamonds>340</finishDiamonds></data><data><id>26</id><upgradeTime>4000</upgradeTime><finishDiamonds>360</finishDiamonds></data><data><id>27</id><upgradeTime>4500</upgradeTime><finishDiamonds>380</finishDiamonds></data><data><id>28</id><upgradeTime>5000</upgradeTime><finishDiamonds>400</finishDiamonds></data><data><id>29</id><upgradeTime>5500</upgradeTime><finishDiamonds>420</finishDiamonds></data><data><id>30</id><upgradeTime>6000</upgradeTime><finishDiamonds>440</finishDiamonds></data><data><id>31</id><upgradeTime>6500</upgradeTime><finishDiamonds>460</finishDiamonds></data><data><id>32</id><upgradeTime>7000</upgradeTime><finishDiamonds>480</finishDiamonds></data><data><id>33</id><upgradeTime>7500</upgradeTime><finishDiamonds>500</finishDiamonds></data><data><id>34</id><upgradeTime>8000</upgradeTime><finishDiamonds>525</finishDiamonds></data><data><id>35</id><upgradeTime>8500</upgradeTime><finishDiamonds>550</finishDiamonds></data><data><id>36</id><upgradeTime>9000</upgradeTime><finishDiamonds>575</finishDiamonds></data><data><id>37</id><upgradeTime>9500</upgradeTime><finishDiamonds>600</finishDiamonds></data><data><id>38</id><upgradeTime>10000</upgradeTime><finishDiamonds>625</finishDiamonds></data><data><id>39</id><upgradeTime>11000</upgradeTime><finishDiamonds>650</finishDiamonds></data><data><id>40</id><upgradeTime>12000</upgradeTime><finishDiamonds>675</finishDiamonds></data><data><id>41</id><upgradeTime>13000</upgradeTime><finishDiamonds>700</finishDiamonds></data><data><id>42</id><upgradeTime>14000</upgradeTime><finishDiamonds>750</finishDiamonds></data><data><id>43</id><upgradeTime>15000</upgradeTime><finishDiamonds>800</finishDiamonds></data><data><id>44</id><upgradeTime>16000</upgradeTime><finishDiamonds>850</finishDiamonds></data><data><id>45</id><upgradeTime>17000</upgradeTime><finishDiamonds>900</finishDiamonds></data><data><id>46</id><upgradeTime>18000</upgradeTime><finishDiamonds>950</finishDiamonds></data><data><id>47</id><upgradeTime>19000</upgradeTime><finishDiamonds>1000</finishDiamonds></data><data><id>48</id><upgradeTime>20000</upgradeTime><finishDiamonds>1050</finishDiamonds></data><data><id>49</id><upgradeTime>22500</upgradeTime><finishDiamonds>1100</finishDiamonds></data><data><id>50</id><upgradeTime>25000</upgradeTime><finishDiamonds>1150</finishDiamonds></data>
</UpgradeTime>
    ";

    public static UpgradeTimeFactory Instance {
        get {
            if (_instance == null) {
                XmlSerializer xs = new XmlSerializer(typeof(UpgradeTimeFactory));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(configXML));
                _instance = xs.Deserialize(ms) as UpgradeTimeFactory;
            }
            return _instance;
        }
    }
}
    