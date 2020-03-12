    
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


public class SupplyInfo {
	public int id; // 编号
	public int supplyLevel; // 补给包等级
	public int supplyAmount; // 补给包数量
	public int onlineAward; // 在线奖励等级
	public int onlineTime; // 触发间隔
}

[XmlRoot("SupplyInfo")]
public class SupplyInfoFactory {
    [XmlElement("data")]
    public SupplyInfo[] SupplyInfoArray {get; set;}
    private static SupplyInfoFactory _instance;

    SupplyInfoFactory() {}

    const string configXML = @"
    <SupplyInfo>
    <data><id>1</id><supplyLevel>1</supplyLevel><supplyAmount>15</supplyAmount><onlineAward>1</onlineAward><onlineTime>60</onlineTime></data><data><id>2</id><supplyLevel>2</supplyLevel><supplyAmount>8</supplyAmount><onlineAward>1</onlineAward><onlineTime>60</onlineTime></data><data><id>3</id><supplyLevel>2</supplyLevel><supplyAmount>15</supplyAmount><onlineAward>1</onlineAward><onlineTime>60</onlineTime></data><data><id>4</id><supplyLevel>3</supplyLevel><supplyAmount>8</supplyAmount><onlineAward>1</onlineAward><onlineTime>60</onlineTime></data><data><id>5</id><supplyLevel>3</supplyLevel><supplyAmount>15</supplyAmount><onlineAward>2</onlineAward><onlineTime>60</onlineTime></data><data><id>6</id><supplyLevel>4</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>3</onlineAward><onlineTime>60</onlineTime></data><data><id>7</id><supplyLevel>5</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>4</onlineAward><onlineTime>61</onlineTime></data><data><id>8</id><supplyLevel>6</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>5</onlineAward><onlineTime>64</onlineTime></data><data><id>9</id><supplyLevel>7</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>6</onlineAward><onlineTime>67</onlineTime></data><data><id>10</id><supplyLevel>8</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>7</onlineAward><onlineTime>70</onlineTime></data><data><id>11</id><supplyLevel>9</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>8</onlineAward><onlineTime>73</onlineTime></data><data><id>12</id><supplyLevel>10</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>9</onlineAward><onlineTime>76</onlineTime></data><data><id>13</id><supplyLevel>11</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>10</onlineAward><onlineTime>79</onlineTime></data><data><id>14</id><supplyLevel>12</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>11</onlineAward><onlineTime>82</onlineTime></data><data><id>15</id><supplyLevel>13</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>12</onlineAward><onlineTime>85</onlineTime></data><data><id>16</id><supplyLevel>14</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>13</onlineAward><onlineTime>88</onlineTime></data><data><id>17</id><supplyLevel>15</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>14</onlineAward><onlineTime>91</onlineTime></data><data><id>18</id><supplyLevel>16</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>15</onlineAward><onlineTime>94</onlineTime></data><data><id>19</id><supplyLevel>17</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>16</onlineAward><onlineTime>97</onlineTime></data><data><id>20</id><supplyLevel>18</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>17</onlineAward><onlineTime>100</onlineTime></data><data><id>21</id><supplyLevel>19</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>18</onlineAward><onlineTime>103</onlineTime></data><data><id>22</id><supplyLevel>20</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>19</onlineAward><onlineTime>106</onlineTime></data><data><id>23</id><supplyLevel>21</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>20</onlineAward><onlineTime>109</onlineTime></data><data><id>24</id><supplyLevel>22</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>21</onlineAward><onlineTime>112</onlineTime></data><data><id>25</id><supplyLevel>23</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>22</onlineAward><onlineTime>115</onlineTime></data><data><id>26</id><supplyLevel>24</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>23</onlineAward><onlineTime>118</onlineTime></data><data><id>27</id><supplyLevel>25</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>24</onlineAward><onlineTime>121</onlineTime></data><data><id>28</id><supplyLevel>26</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>25</onlineAward><onlineTime>124</onlineTime></data><data><id>29</id><supplyLevel>27</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>26</onlineAward><onlineTime>127</onlineTime></data><data><id>30</id><supplyLevel>28</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>27</onlineAward><onlineTime>130</onlineTime></data><data><id>31</id><supplyLevel>29</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>28</onlineAward><onlineTime>133</onlineTime></data><data><id>32</id><supplyLevel>30</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>29</onlineAward><onlineTime>136</onlineTime></data><data><id>33</id><supplyLevel>31</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>30</onlineAward><onlineTime>139</onlineTime></data><data><id>34</id><supplyLevel>32</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>31</onlineAward><onlineTime>142</onlineTime></data><data><id>35</id><supplyLevel>33</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>32</onlineAward><onlineTime>145</onlineTime></data><data><id>36</id><supplyLevel>34</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>33</onlineAward><onlineTime>148</onlineTime></data><data><id>37</id><supplyLevel>35</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>34</onlineAward><onlineTime>151</onlineTime></data><data><id>38</id><supplyLevel>36</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>35</onlineAward><onlineTime>154</onlineTime></data><data><id>39</id><supplyLevel>37</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>36</onlineAward><onlineTime>157</onlineTime></data><data><id>40</id><supplyLevel>38</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>37</onlineAward><onlineTime>160</onlineTime></data><data><id>41</id><supplyLevel>39</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>38</onlineAward><onlineTime>163</onlineTime></data><data><id>42</id><supplyLevel>40</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>39</onlineAward><onlineTime>166</onlineTime></data><data><id>43</id><supplyLevel>41</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>40</onlineAward><onlineTime>169</onlineTime></data><data><id>44</id><supplyLevel>42</supplyLevel><supplyAmount>10</supplyAmount><onlineAward>41</onlineAward><onlineTime>165</onlineTime></data><data><id>45</id><supplyLevel>42</supplyLevel><supplyAmount>12</supplyAmount><onlineAward>41</onlineAward><onlineTime>160</onlineTime></data><data><id>46</id><supplyLevel>42</supplyLevel><supplyAmount>14</supplyAmount><onlineAward>41</onlineAward><onlineTime>155</onlineTime></data><data><id>47</id><supplyLevel>42</supplyLevel><supplyAmount>16</supplyAmount><onlineAward>41</onlineAward><onlineTime>150</onlineTime></data><data><id>48</id><supplyLevel>42</supplyLevel><supplyAmount>18</supplyAmount><onlineAward>41</onlineAward><onlineTime>145</onlineTime></data><data><id>49</id><supplyLevel>42</supplyLevel><supplyAmount>20</supplyAmount><onlineAward>41</onlineAward><onlineTime>140</onlineTime></data><data><id>50</id><supplyLevel>42</supplyLevel><supplyAmount>30</supplyAmount><onlineAward>41</onlineAward><onlineTime>120</onlineTime></data>
</SupplyInfo>
    ";

    public static SupplyInfoFactory Instance {
        get {
            if (_instance == null) {
                XmlSerializer xs = new XmlSerializer(typeof(SupplyInfoFactory));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(configXML));
                _instance = xs.Deserialize(ms) as SupplyInfoFactory;
            }
            return _instance;
        }
    }
}
    